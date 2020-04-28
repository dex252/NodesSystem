using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NodesDLL;

namespace Asp
{
    public class ConnectionModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public string connectionTree { get; set; }
        [BindProperty]
        public int? groupId { get; set; }
        [BindProperty]
        public int? nodeId { get; set; }
        public IService service { get; set; }
        public List<NodesDLL.Models.Groups> connList { get; set; }

        public ConnectionModel(IService service)
        {
            this.service = service;
            this.connList = service.Groups.Get();
        }
        public IActionResult OnGet(int? groupId, int? nodeId)
        {
            if (nodeId == null) return RedirectToPage("../Error");

            this.groupId = groupId;
            this.nodeId = nodeId;

            ViewData["Connections"] = connList
                .Select(n => new SelectListItem
                {
                    Value = n.id.ToString(),
                    Text = n.name.ToString()
                }).ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            var connectionGroup = connList.FirstOrDefault(e => e.id.ToString() == connectionTree);

            if (connectionGroup == null || nodeId == null) return RedirectToPage("../Main", new { groupId = groupId });

            var bonds = service.Bonds.Get(connectionGroup.id);
            if (bonds == null) return RedirectToPage("./Error");

            Node node = new Node(bonds);
            List<int?> allId = new List<int?>();
            node.MoveNode(ref allId);

            foreach (var id in allId)
            {
                var searchNode = node.Find(id);
                if (searchNode != null)
                {
                    searchNode.Unit = service.Units.Get(id);
                }
            }

            var result = service.Bonds.ConnectionNode(node, nodeId);

            if (result)
            {
                return RedirectToPage("../Main", new { groupId = groupId });
            }

            TempData["Message"] = "К сожалению дерево не было присоединено...";
            return Page();
        }

    }
}