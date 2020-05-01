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

namespace kennel_bambino.web.Pages.Admin.Pets
{
    public class CreateModel : PageModel
    {
        private readonly IPetService _petService;
        private readonly IGroupService _groupService;
        private readonly IEyeColorService _eyeColorService;
        private readonly IBodyTypeService _bodyTypeService;
        private readonly IPatternService _patternService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IPetService petService,
            IGroupService groupService,
            IPatternService patternService,
            IBodyTypeService bodyTypeService,
            IEyeColorService eyeColorService,
            ILogger<CreateModel> logger)
        {
            _petService = petService;
            _bodyTypeService = bodyTypeService;
            _eyeColorService = eyeColorService;
            _patternService = patternService;
            _groupService = groupService;
            _logger = logger;
        }

        [BindProperty]
        public Pet Pet { get; set; }

        public List<IFormFile> Images { get; set; }

        public SelectList BodyTypeSelectList { get; set; }

        public SelectList EyeColorSelectList { get; set; }

        public SelectList PatternSelectList { get; set; }

        public SelectList GroupSelectList { get; set; }

        public async Task OnGetAsync()
        {
            await SeedInitialValues();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _petService.AddPetAsync(Pet, Images) != null)
                {
                    TempData["Success"] = "New Pet successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"Pet {nameof(CreateModel)} database error! ");

                    await SeedInitialValues();

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"Group {nameof(CreateModel)} invalid inputs! ");

            await SeedInitialValues();

            return Page();
        }

        private async Task SeedInitialValues()
        {
            BodyTypeSelectList = new SelectList(await _bodyTypeService.GetBodyTypeSelectListAsync(), "Value", "Text");

            EyeColorSelectList = new SelectList(await _eyeColorService.GetEyeColorSelectListAsync(), "Value", "Text");

            PatternSelectList = new SelectList(await _patternService.GetPatternSelectListAsync(), "Value", "Text");

            GroupSelectList = new SelectList(await _groupService.GetGroupSelectListAsync(), "Value", "Text");
        }

    }
}