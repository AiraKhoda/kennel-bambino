using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.EyeColors
{
    public class EditModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;
        private readonly ILogger<EditModel> _logger;
        public EditModel(IEyeColorService eyeColorService,ILogger<EditModel> logger)
        {
            _eyeColorService = eyeColorService;
            _logger = logger;
        }

        [BindProperty]
        public EyeColor EyeColor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            EyeColor = await _eyeColorService.GetEyeColorByIdAsync(id.Value);

            if (EyeColor == null)
            {
                return NotFound();
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (await _eyeColorService.UpdateEyeColorAsync(EyeColor) != null)
                {
                    TempData["Success"] = "EyeColor successfully updated.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"EyeColor {nameof(EditModel)} database error! ");

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"EyeColor {nameof(EditModel)} database error! ");

            return Page();


        }
    }
}