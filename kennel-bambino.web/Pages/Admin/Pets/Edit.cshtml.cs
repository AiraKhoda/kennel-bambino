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
    public class EditModel : PageModel
    {
        private readonly IPetService _petService;
        private readonly IGroupService _groupService;
        private readonly IEyeColorService _eyeColorService;
        private readonly IBodyTypeService _bodyTypeService;
        private readonly IPatternService _patternService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IPetService petService,
              IGroupService groupService,
              IPatternService patternService,
              IBodyTypeService bodyTypeService,
              IEyeColorService eyeColorService,
              ILogger<EditModel> logger)
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

        public SelectList SubGroupSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Pet = _petService.GetPetById(id.Value);

            await SeedInitialValues(Pet.BodyTypeId, Pet.EyeColorId, Pet.PatternId, Pet.GroupId, Pet.SubGroupId.Value);

            if (Pet == null)
            {
                return NotFound();
            }

            return Page();

        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _petService.UpdatePetAsync(Pet, Images) != null)
                {
                    TempData["Success"] = "New Group successfully updated.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"Pet {nameof(CreateModel)} database error! ");

                    await SeedInitialValues(Pet.BodyTypeId, Pet.EyeColorId, Pet.PatternId, Pet.GroupId, Pet.SubGroupId.Value);


                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"Pet {nameof(CreateModel)} invalid inputs! ");

            await SeedInitialValues(Pet.BodyTypeId, Pet.EyeColorId, Pet.PatternId, Pet.GroupId, Pet.SubGroupId.Value);


            return Page();
        }







        private async Task SeedInitialValues(int bodyTypeId, int eyeColorId, int patternId, int groupId, int subGroupId)
        {
            BodyTypeSelectList = new SelectList(await _bodyTypeService.GetBodyTypeSelectListAsync(), "Value", "Text", bodyTypeId);

            EyeColorSelectList = new SelectList(await _eyeColorService.GetEyeColorSelectListAsync(), "Value", "Text", eyeColorId);

            PatternSelectList = new SelectList(await _patternService.GetPatternSelectListAsync(), "Value", "Text", patternId);

            GroupSelectList = new SelectList(await _groupService.GetGroupSelectListAsync(), "Value", "Text", groupId);

            SubGroupSelectList = new SelectList(await _groupService.GetSubGroupSelectListAsync(groupId), "Value", "Text", subGroupId);
        }
    }
}