using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace Asp
{
    public class DetailModel : PageModel
    {
        public INode node { get; set; }
        private IService service { get; }
        [TempData]
        public string Message { get; set; }

        public DetailModel(IService service)
        {
            this.service = service;
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