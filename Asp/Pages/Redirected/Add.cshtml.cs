using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NodesDLL;

namespace Asp
{
    public class AddModel : PageModel
    {
        private IService service;
        public IHtmlHelper htmlHelper { get; }
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public Node node { get; set; }
        public List<NodesDLL.Groups> types { get; set; }
      
        public AddModel(IService service, IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.service = service;
            this.types = service.Types.Get();
            this.node = new Node();
        }
        public IActionResult OnGet(int parentId, int? groupId)
        {
            this.node.parentId = parentId;
            this.node.Unit.date_create = DateTime.Today;
            this.node.groupId = groupId;
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

            var newId = service.Nodes.Create(node);

            if (newId == null)
            {
                TempData["Message"] = "Ошибка при создании узла";
                return RedirectToPage("./Add", new {parentId = node.parentId, groupId= node.groupId});
            }

            node.nodeId = newId;
            TempData["Message"] = "Узел создан успешно!";
            return RedirectToPage("./Detail", new { nodeId = node.nodeId, groupId = node.groupId });
        }
    }
}