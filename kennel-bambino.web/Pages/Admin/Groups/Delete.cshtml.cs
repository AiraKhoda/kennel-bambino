﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace kennel_bambino.web.Pages.Admin.Groups
{
    public class DeleteModel : PageModel
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(IGroupService groupService, ILogger<DeleteModel> logger)
        {
            _logger = logger;
            _groupService = groupService;
        }
        public Group Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Group = await _groupService.GetGroupByIdAsync(id.Value);

            if (Group == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            TempData["Success"] = "Group successfully removed.";
            await _groupService.RemoveGroupAsync(id);

            return RedirectToPage("Index");
        }
    }
}