using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.EyeColors
{
    public class CreateModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(IEyeColorService eyeColorService, ILogger<CreateModel> logger)
        {
            _eyeColorService = eyeColorService;
            _logger = logger;
        }

        [BindProperty]
        public EyeColor EyeColor { get; set; }
       

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (await _eyeColorService.AddEyeColorAsync(EyeColor) != null)
                {
                    TempData["Success"] = "New EyeColor successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"EyeColor {nameof(CreateModel)} database error! ");

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"EyeColor {nameof(CreateModel)} database error! ");

            return Page();


        }
    }
}