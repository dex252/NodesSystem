using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp
{
    public class DetailModel : PageModel
    {
        public IActionResult OnGet(int? nodeId)
        {
            if (nodeId.HasValue)
            {
                return Page();
            }

            return RedirectToPage("../Error");
        }
    }
}