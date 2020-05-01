using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface IPatternService
    {
        Pattern AddPattern(Pattern pattern);
        Task<Pattern> AddPatternAsync(Pattern pattern);
        List<Pattern> GetAllPatterns();
        Task<List<Pattern>> GetAllPatternsAsync();
        List<SelectListItem> GetPatternSelectList();
        Task<List<SelectListItem>> GetPatternSelectListAsync();
        Pattern GetPatternById(int patternId);
        Task<Pattern> GetPatternByIdAsync(int patternId);
        Pattern UpdatePattern(Pattern pattern);
        Task<Pattern> UpdatePatternAsync(Pattern pattern);
        void RemovePattern(int patternId);
        Task RemovePatternAsync(int patternId);
        int PatternsCount();
        Task<int> PatternsCountAsync();
    }
}
