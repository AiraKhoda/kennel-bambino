﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace kennel_bambino.web.Pages.Admin.Groups
{
    public class CreateModel : PageModel
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IGroupService groupService, ILogger<CreateModel> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        [BindProperty]
        public Group Group { get; set; }
        public int? ParrentId { get; private set; }
        public IFormFile Image { get; set; }

        public void OnGet(int? id)
        {
            FeedParentId(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _groupService.AddGroupAsync(Image, Group) != null)
                {
                    TempData["Success"] = "New Group successfully added.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"Group {nameof(CreateModel)} database error! ");

                    FeedParentId(Group.ParentId);

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"Group {nameof(CreateModel)} invalid inputs! ");

            FeedParentId(Group.ParentId);

            return Page();
        }


        private void FeedParentId(int? parentId)
        {
            ParrentId = parentId;
        }

    }
}