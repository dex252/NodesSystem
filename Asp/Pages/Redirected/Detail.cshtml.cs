using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace Asp
{
    public class DetailModel : PageModel
    {
        public INode node { get; set; }
        public INode parent { get; set; }
        private IService service { get; }
        [TempData]
        public string Message { get; set; }

        public DetailModel(IService service)
        {
            this.service = service;
        }
        public IActionResult OnGet(int? nodeId)
        {
            if (!nodeId.HasValue) return RedirectToPage("../Error");

            node = new Node {nodeId = nodeId, Unit = service.Units.Get(nodeId)};

            return Page();
        }
    }
}