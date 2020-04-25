using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NodesDLL;
using NodesDLL.Models;

namespace Asp
{
    public class AddTreeModel : PageModel
    {
        private IService service;
        public IHtmlHelper htmlHelper { get; }
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public Node node { get; set; }
        [BindProperty]
        public Groups group { get; set; }
        public List<NodesDLL.Types> types { get; set; }
        public AddTreeModel(IService service, IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.service = service;
            this.types = service.Types.Get();
            this.node = new Node();
            this.group = new Groups()
            {
                date = DateTime.Today
            };
        }
        public IActionResult OnGet()
        {
            this.node.parentId = null;
            this.node.Unit.date_create = DateTime.Today;
        
            ViewData["Types"] = types
                .Select(n => new SelectListItem
                {
                    Value = n.id.ToString(),
                    Text = n.name.ToString()
                }).ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            ViewData["Types"] = types
                .Select(n => new SelectListItem
                {
                    Value = n.id.ToString(),
                    Text = n.name.ToString()
                }).ToList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = service.Groups.Create(node, group);

            if (success == false)
            {
                TempData["Message"] = "Ошибка при создании дерева";
                return RedirectToPage("./AddTree");
            }

            TempData["Message"] = "Дерево успешно создано!";
            return RedirectToPage("../Index");
        }
    }
}