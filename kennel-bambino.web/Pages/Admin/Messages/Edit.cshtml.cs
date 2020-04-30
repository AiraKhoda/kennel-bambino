using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace kennel_bambino.web.Pages.Admin.Messages
{
    public class EditModel : PageModel
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<EditModel> _logger;
        public EditModel(IMessageService messageService, ILogger<EditModel> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        [BindProperty]
        public Contact Contact { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Contact = await _messageService.GetContactByIdAsync(id.Value);

            if (Contact == null)
            {
                return NotFound();
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (await _messageService.UpdateContactAsync(Contact) != null)
                {
                    TempData["Success"] = "Message successfully updated.";
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Database error,Contact the developer to fix the error.";

                    _logger.LogError($"Message {nameof(EditModel)} database error! ");

                    return Page();
                }
            }

            ViewData["Message"] = "Your input is not valid";

            _logger.LogError($"Message {nameof(EditModel)} database error! ");

            return Page();


        }
    }
}