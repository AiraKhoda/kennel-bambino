using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.Patterns
{
    public class CreateModel : PageModel
    {
        private readonly IPatternService _patternService;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(IPatternService patternService, ILogger<CreateModel> logger)
        {
            _patternService = patternService;
            _logger = logger;
        }

        [BindProperty]
        public Pattern Pattern { get; set; }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (await _patternService.AddPatternAsync(Pattern) != null)
                {
                    TempData["Success"] = "New Pattern successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"Pattern {nameof(CreateModel)} database error! ");

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"Pattern {nameof(CreateModel)} database error! ");

            return Page();


        }
    }
}