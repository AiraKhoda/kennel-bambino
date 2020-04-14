using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.BodyTypes
{
    public class CreateModel : PageModel
    {

        private readonly IBodyTypeService _bodyTypeService;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(IBodyTypeService bodyTypeService, ILogger<CreateModel> logger)
        {
            _bodyTypeService = bodyTypeService;
            _logger = logger;
        }

        [BindProperty]
        public BodyType BodyType { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (await _bodyTypeService.AddBodyTypeAsync(BodyType) != null)
                {
                    TempData["Success"] = "New BodyType successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"BodyType {nameof(CreateModel)} database error! ");

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"BodyType {nameof(CreateModel)} invalid inputs! ");

            return Page();


        }
    }
}