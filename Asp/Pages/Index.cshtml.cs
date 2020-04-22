using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;
using Test;

namespace AspNet
{
    public class IndexModel : PageModel
    {
        public INode node { get; set; }

        public void OnGet()
        {
            this.node = new NodeCreate().CreateNodes();
           
            List<Node> refresh = new List<Node>();
            node.RefreshLevels(ref refresh);

            this.node = node;
        }
    }
}