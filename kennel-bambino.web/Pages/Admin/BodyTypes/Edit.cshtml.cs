using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.BodyTypes
{
    public class EditModel : PageModel
    {

        private readonly IBodyTypeService _bodyTypeService;
        private readonly ILogger<EditModel> _logger;
        public EditModel(IBodyTypeService bodyTypeService, ILogger<EditModel> logger)
        {
            _bodyTypeService = bodyTypeService;
            _logger = logger;
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (await _bodyTypeService.UpdateBodyTypeAsync(BodyType) != null)
                {
                    TempData["Success"] = "BodyType successfully updated.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"BodyType {nameof(EditModel)} database error! ");

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"BodyType {nameof(EditModel)} database error! ");

            return Page();


        }
    }
}