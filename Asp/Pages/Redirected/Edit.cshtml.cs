using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NodesDLL;

namespace Asp
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Node node { get; set; }
        private IService service { get; }
        public IHtmlHelper htmlHelper { get; }
        public List<NodesDLL.Types> types { get; set; }

        public EditModel(IService service, IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.service = service;
            this.types = service.Types.Get();
        }
        public IActionResult OnGet(int? nodeId, int? groupId)
        {
            if (!nodeId.HasValue || !groupId.HasValue) return RedirectToPage("../Error");

            ViewData["Types"] = types
                .Select(n => new SelectListItem
                {
                    Value = n.id.ToString(),
                    Text = n.name.ToString()
                }).ToList();

            node = new Node
            {
                nodeId = nodeId,
                groupId = groupId,
                Unit = service.Units.Get(nodeId)
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            var success = service.Nodes.Update(this.node);

            if (success == false)
            {
                TempData["Message"] = "Ошибка при обновлении узла!";
                return RedirectToPage("./Detail", new { nodeId = node.nodeId, groupId = node.groupId });
            }

            TempData["Message"] = "Узел успешно обновлен!";
            return RedirectToPage("./Detail", new { nodeId = node.nodeId, groupId = node.groupId });
        }
    }
}