using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace Asp
{
    public class MainModel : PageModel
    {
        public IService service { get; }
        public INode node { get; set; }

        public MainModel(IService service)
        {
            this.service = service;
        }
        public IActionResult OnGet(int? groupId)
        {
            if (groupId == null) return RedirectToPage("./Error");
            
            var bonds = service.Bonds.Get(groupId);

            if (bonds == null) return RedirectToPage("./Error");

            this.node = new Node(bonds);
            this.node.groupId = groupId;
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

                searchNode.groupId = groupId;
                searchNode.Edit(new Unit()
                {
                    id = n.Key,
                    name = searchNode.Unit.name,
                    fullname = n.Value
                });
            }

            return Page();

        }
    }
}