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

        public DetailTreeModel(IService service, IController controller)
        {
            this.service = service;
            this.controller = controller;
        }
        public IActionResult OnGet(int? nodeId)
        {
            
            if (!nodeId.HasValue) return RedirectToPage("../Error");
            this.node = controller.node.Find(nodeId);

            return Page();
        }


        public IActionResult OnPostReturn()
        {
            return RedirectToPage("./ShowJsonTree");
        }
    }
}