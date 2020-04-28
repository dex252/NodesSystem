using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NodesDLL;

namespace Asp
{
    public class ShowJsonTreeModel : PageModel
    {
        public IController controller { get; }
        public List<string> pathList { get; set; }
        public Node node { get; set; }
        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public string file { get; set; }

        public ShowJsonTreeModel(IController controller)
        {
            this.controller = controller;
        }
        public void OnGet()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string[] fileEntries = Directory.GetFiles(path);
            pathList = new List<string>();
            foreach (var file in fileEntries)
            {
                if (System.IO.Path.GetExtension(file) == ".json")
                {
                    pathList.Add(file);
                }
            }

            ViewData["Types"] = pathList
                .Select(n => new SelectListItem
                {
                    Value = n.ToString(),
                    Text = n.ToString()
                }).ToList();

            if (controller.json != null)
            {
                controller.state = false;
                Console.WriteLine(controller.json);

                var json = System.IO.File.ReadAllText(controller.json);

                node = Newtonsoft.Json.JsonConvert.DeserializeObject<Node>(json, new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                });

                controller.node = node;
            }
        }

        public IActionResult OnPost()
        {
            if (file == null) return Page();

            controller.json = file;
            controller.state = true;

            return RedirectToPage("./ShowJsonTree");
        }

        public IActionResult OnPostSaveOnServer()
        {
            controller.state = true;

            return RedirectToPage("./LoadTree");
        }
    }
}