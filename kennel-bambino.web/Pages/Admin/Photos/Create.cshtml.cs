using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.Photos
{
    public class CreateModel : PageModel
    {

        private readonly IPhotoService _photoService;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(IPhotoService photoService, ILogger<CreateModel> logger)
        {
            _photoService = photoService;
            _logger = logger;
        }

        [BindProperty]
        public Photo Photo { get; set; }
        public SelectList PetsSelectList { get; set; }
        public IFormFile Image { get; set; }


        public async Task OnGet()
        {
            await FillPetsSelectListItem();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _photoService.AddPhotoAsync(Photo, Image) != null)
                {
                    TempData["Success"] = "New Photo successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"Photo {nameof(CreateModel)} database error! ");

                    await FillPetsSelectListItem();

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"Photo {nameof(CreateModel)} invalid inputs! ");

            await FillPetsSelectListItem();

            return Page();
        }



        private async Task FillPetsSelectListItem()
        {
            PetsSelectList = new SelectList(await _photoService.GetPetSelectListItemAsync(), "Value", "Text");
        }
    }
}