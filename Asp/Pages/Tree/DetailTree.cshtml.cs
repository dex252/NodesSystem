using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace Asp
{
    public class DetailTreeModel : PageModel
    {
        public INode node { get; set; }
        private IService service { get; }
        public IController controller { get; }

        [TempData]
        public string Message { get; set; }

        public DetailTreeModel(IService service, IController controller)
        {
            this.service = service;
            this.controller = controller;
        }
        public IActionResult OnGet(int? nodeId, int? groupId)
        {
            if (!nodeId.HasValue || !groupId.HasValue) return RedirectToPage("../Error");

            node = new Node {
                nodeId = nodeId, 
                groupId = groupId,
                Unit = service.Units.Get(nodeId)

            };

            return Page();
        }
    }
}