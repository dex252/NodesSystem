using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;
using NodesDLL.Models;

namespace Asp
{
    public class LoadTreeModel : PageModel
    {
        public IService service { get; }
        public IController controller { get; }
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public Node node { get; set; }
        [BindProperty]
        public Groups group { get; set; }

        public LoadTreeModel(IService service, IController controller)
        {
            this.service = service;
            this.controller = controller;
        }
        public void OnGet()
        {
            if (controller.json != null)
            {
                var json = System.IO.File.ReadAllText(controller.json);
                node = Newtonsoft.Json.JsonConvert.DeserializeObject<Node>(json, new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                });

                group = new Groups()
                {
                    date = DateTime.Now
                };
            }
        }
        public IActionResult OnPost()
        {
            if (node.id == 0 && node.groupId == null)
            {
                var json = System.IO.File.ReadAllText(controller.json);
                node = Newtonsoft.Json.JsonConvert.DeserializeObject<Node>(json, new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                });
            }

            var result = service.Groups.LoadTree(node, group);

            if (result)
            {
                TempData["Message"] = "Дерево успешно сохранено на сервере!";
                return RedirectToPage("./ShowJsonTree");
            }

            TempData["Message"] = "Ошибка сохранения...";
            return RedirectToPage("./LoadTree");
        }
        public IActionResult OnPostReturn()
        {
            return RedirectToPage("./ShowJsonTree");
        }
    }
}