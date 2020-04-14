using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kennel_bambino.web.Pages.Admin.EyeColors
{
    public class IndexModel : PageModel
    {
        private readonly IEyeColorService _eyeColorService;
        public IndexModel(IEyeColorService eyeColorService)
        {
            _eyeColorService = eyeColorService;
        }

        public List<EyeColor> EyeColors { get; private set; }
        public int EyeColorsCount { get; set; }

        public async Task OnGet()
        {
            EyeColors = await _eyeColorService.GetAllEyeColorsAsync();

            EyeColorsCount = await _eyeColorService.EyeColorsCountAsync();

        }
    }
}