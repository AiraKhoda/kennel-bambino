using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface IEyeColorService
    {
        EyeColor AddEyeColor(EyeColor eyeColor);
        Task<EyeColor> AddEyeColorAsync(EyeColor eyeColor);
        List<EyeColor> GetAllEyeColors();
        Task<List<EyeColor>> GetAllEyeColorsAsync();
        List<SelectListItem> GetEyeColorSelectList();
        Task<List<SelectListItem>> GetEyeColorSelectListAsync();
        EyeColor GetEyeColorById(int eyeColorId);
        Task<EyeColor> GetEyeColorByIdAsync(int eyeColorId);
        EyeColor UpdateEyeColor(EyeColor eyeColor);
        Task<EyeColor> UpdateEyeColorAsync(EyeColor eyeColor);
        void RemoveEyeColor(int eyeColorId);
        Task RemoveEyeColorAsync(int eyeColorId);
        int EyeColorsCount();
        Task<int> EyeColorsCountAsync();
    }
}
