using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodesDLL;

namespace Asp
{
    public class DeleteTreeModel : PageModel
    {
        public IService service { get; }
        [BindProperty]
        public int groupId { get; set; }
        [TempData]
        public string Message { get; set; }

        public DeleteTreeModel(IService service)
        {
            this.service = service;
        }
        public IActionResult OnGet(int? groupId)
        {
            if (!groupId.HasValue) return RedirectToPage("../Error");

            this.groupId = (int)groupId;

            return Page();
        }

        public IActionResult OnPost()
        {
            bool success = service.Groups.Delete(groupId);

            if (!success)
            {
                TempData["Message"] = $"Ошибка при удалении записи {groupId}";
                return RedirectToPage("./DeleteTree", groupId);
            }

            return RedirectToPage("../Index");
        }
    }
}