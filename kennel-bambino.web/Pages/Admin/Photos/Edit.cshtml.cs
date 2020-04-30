using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace kennel_bambino.web.Pages.Admin.Photos
{
    public class EditModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly ILogger<EditModel> _logger;
        public EditModel(IPhotoService photoService, ILogger<EditModel> logger)
        {
            _photoService = photoService;
            _logger = logger;
        }

        [BindProperty]
        public Photo Photo { get; set; }
        public SelectList PetsSelectList { get; set; }
        public IFormFile Image { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Photo = await _photoService.GetPhotoByIdAsync(id.Value);

            await FillPetsSelectListItem();

            if (Photo == null)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _photoService.UpdatePhotoAsync(Photo, Image) != null)
                {
                    TempData["Success"] = "Photo successfully Updated.";
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