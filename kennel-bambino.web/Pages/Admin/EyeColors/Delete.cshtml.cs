using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.EyeColors
{
    public class DeleteModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;
        public DeleteModel(IEyeColorService eyeColorService)
        {
            _eyeColorService = eyeColorService;
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
        public async Task<IActionResult> OnPostAsync(int id)
        {
            TempData["Success"] = "EyeColor successfully removed.";
            await _eyeColorService.RemoveEyeColorAsync(id);

            return RedirectToPage("Index");
        }
    }
}