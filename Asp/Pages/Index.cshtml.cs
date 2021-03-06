﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;
using NodesDLL.Models;

namespace AspNet
{
    public class IndexModel : PageModel
    {
        private IService service { get; }
        public List<NodesDLL.Models.Groups> groups { get; set; }
        [TempData]
        public string Message { get; set; }

        public IndexModel(IService service)
        {
            this.service = service;
        }
        public IActionResult OnGet()
        {
            groups = service.Groups.Get();

            return Page();
        }
    }
}