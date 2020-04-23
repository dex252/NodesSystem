using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace AspNet
{
    public class IndexModel : PageModel
    {
        public IService service { get; }
        public INode node { get; set; }

        public IndexModel(IService service)
        {
            this.service = service;
        }
        public void OnGet()
        {
            var bonds = service.Bonds.Get();

            this.node = new Node(bonds);
            if (this.node != null)
            {
                List<int?> items = new List<int?>();
                node.MoveNode(ref items);

                var names = service.Units.GetNames(items);

                foreach (var n in names)
                {
                    var searchNode = node.Find(n.Key);
                   
                    searchNode.Edit(new Unit()
                    {
                        id = n.Key,
                        name = n.Value
                    });
                }
            }
        }
    }
}