﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace Asp
{
    public class MainModel : PageModel
    {
        public IService service { get; }
        public IController controller { get; }
        private readonly IHostingEnvironment hosting;

        public Node node { get; set; }

        public MainModel(IService service, IController controller, IHostingEnvironment hosting)
        {
            this.service = service;
            this.controller = controller;
            this.hosting = hosting;
        }
        public IActionResult OnGet(int? groupId)
        {
            if (groupId == null) return RedirectToPage("./Error");
            controller.groupId = groupId;
            var bonds = service.Bonds.Get(groupId);

            if (bonds == null) return RedirectToPage("./Error");

            this.node = new Node(bonds);
            this.node.groupId = groupId;
            List<int?> items = new List<int?>();
            node.MoveNode(ref items);

            var names = service.Units.GetNames(items);
            var fullNames = service.Units.GetFullNames(items);
            var positions = service.Units.GetPositions(items);

            for (int i = 0; i < names.Count; i++)
            {
                var key = names.ElementAt(i).Key;
                var searchNode = node.Find(key);

                searchNode.groupId = groupId;
                string name;
                string fullname;
                string position;
                names.TryGetValue(key, out name);
                fullNames.TryGetValue(key, out fullname);
                positions.TryGetValue(key, out position);

                searchNode.Edit(new Unit()
                {
                    id = key,
                    name = name,
                    fullname = fullname,
                    position = position
                });

            }

            controller.node = node;

            return Page();
        }
      
        public IActionResult OnPostJsonResult()
        {
            node = controller.node;

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

            var path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\nodes-" + Guid.NewGuid() + ".json";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(node);

            System.IO.File.WriteAllText(path, json);

            return Page();
        }
      
        public IActionResult OnPostPdfResult()
        {
            return Page();
        }
    }
}