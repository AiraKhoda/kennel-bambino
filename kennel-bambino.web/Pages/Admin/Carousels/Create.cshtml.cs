using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace kennel_bambino.web.Pages.Admin.Carousels
{
    public class CreateModel : PageModel
    {
        private readonly ICarouselService _carouselService;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(ICarouselService carouselService, ILogger<CreateModel> logger)
        {
            _carouselService = carouselService;
            _logger = logger;
        }

        [BindProperty]
        public Carousel Carousel { get; set; }
        public IFormFile Image { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _carouselService.AddCarouselAsync(Carousel, Image) != null)
                {
                    TempData["Success"] = "New Carousel successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"Carousel {nameof(CreateModel)} database error! ");

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"Carousel {nameof(CreateModel)} invalid inputs! ");

            return Page();
        }
    }
}