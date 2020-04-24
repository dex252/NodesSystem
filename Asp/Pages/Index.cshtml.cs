using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult OnGet()
        {
            var bonds = service.Bonds.Get();

            if (bonds != null)
            {
                this.node = new Node(bonds);
                List<int?> items = new List<int?>();
                node.MoveNode(ref items);

                var names = service.Units.GetNames(items);
                var fullNames = service.Units.GetFullNames(items);

                foreach (var n in names)
                {
                    var searchNode = node.Find(n.Key);
                   
                    searchNode.Edit(new Unit()
                    {
                        id = n.Key,
                        name = n.Value
                    });
                }

                foreach (var n in fullNames)
                {
                    var searchNode = node.Find(n.Key);

                    searchNode.Edit(new Unit()
                    {
                        id = n.Key,
                        name = searchNode.Unit.name,
                        fullname = n.Value
                    });
                }

                return Page();
            }
            
            return RedirectToPage("./Error");
            
        }
    }
}