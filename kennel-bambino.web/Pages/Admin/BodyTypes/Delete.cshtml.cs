using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.BodyTypes
{
    public class DeleteModel : PageModel
    {
        private readonly IBodyTypeService _bodyTypeService;
        public DeleteModel(IBodyTypeService bodyTypeService)
        {
            _bodyTypeService = bodyTypeService;
        }

        [BindProperty]
        public BodyType BodyType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            BodyType = await _bodyTypeService.GetBodyTypeByIdAsync(id.Value);

            if (BodyType == null)
            {
                return NotFound();
            }

            return Page();

        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            TempData["Success"] = "BodyType successfully removed.";
            await _bodyTypeService.RemoveBodyTypeAsync(id);

            return RedirectToPage("Index");
        }
    }
}