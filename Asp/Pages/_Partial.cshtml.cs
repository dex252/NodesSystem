using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace AspNet
{
    public class _PartialModel : PageModel
    {
        public INode node { get; set; }
        public void OnGet(INode node)
        {
            this.node = node;
        }
    }
}