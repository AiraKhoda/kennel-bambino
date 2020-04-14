using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.Patterns
{
    public class EditModel : PageModel
    {
        private readonly IPatternService _patternService;
        private readonly ILogger<EditModel> _logger;
        public EditModel(IPatternService patternService,ILogger<EditModel> logger)
        {
            _patternService = patternService;
            _logger = logger;
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (await _patternService.UpdatePatternAsync(Pattern) != null)
                {
                    TempData["Success"] = "Pattern successfully updated.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"Pattern {nameof(EditModel)} database error! ");

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"Pattern {nameof(EditModel)} database error! ");

            return Page();


        }
    }
}