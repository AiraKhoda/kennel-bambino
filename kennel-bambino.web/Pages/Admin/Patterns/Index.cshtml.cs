using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.Patterns
{
    public class IndexModel : PageModel
    {
        private readonly IPatternService _patternService;
        public IndexModel(IPatternService patternService)
        {
            _patternService = patternService;
        }

        public List<Pattern> Patterns { get; private set; }
        public int PatternsCount { get; set; }

        public async Task OnGet()
        {
            Patterns = await _patternService.GetAllPatternsAsync();

            PatternsCount = await _patternService.PatternsCountAsync();

        }
    }
}