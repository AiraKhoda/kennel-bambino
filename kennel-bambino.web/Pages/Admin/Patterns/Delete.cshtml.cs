using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.Patterns
{
    public class DeleteModel : PageModel
    {
        private readonly IPatternService _patternService;
        public DeleteModel(IPatternService patternService)
        {
            _patternService = patternService;
        }

        [BindProperty]
        public Pattern Pattern { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Pattern = await _patternService.GetPatternByIdAsync(id.Value);

            if (Pattern == null)
            {
                return NotFound();
            }

            return Page();

        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            TempData["Success"] = "Pattern successfully removed.";
            await _patternService.RemovePatternAsync(id);

            return RedirectToPage("Index");
        }
    }
}