using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace Asp
{
    public class DetailModel : PageModel
    {
        public INode node { get; set; }
        private IService service { get; }

        public DetailModel(IService service)
        {
            this.service = service;
        }
        public IActionResult OnGet(int? nodeId)
        {
            if (!nodeId.HasValue) return RedirectToPage("../Error");

            node = new Node {nodeId = nodeId, Unit = service.Units.Get(nodeId)};

            if (node.Unit == null ) return RedirectToPage("../Error");
                
            return Page();
        }
    }
}