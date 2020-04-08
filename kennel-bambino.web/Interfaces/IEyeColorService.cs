using kennel_bambino.web.Models;
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
        IEnumerable<EyeColor> GetAllEyeColors();
        Task<IEnumerable<EyeColor>> GetAllEyeColorsAsync();
        EyeColor GetEyeColorById(int eyeColorId);
        Task<EyeColor> GetEyeColorByIdAsync(int eyeColorId);
        EyeColor UpdateEyeColor(EyeColor eyeColor);
        Task<EyeColor> UpdateEyeColorAsync(EyeColor eyeColor);
        void RemoveEyeColor(int eyeColorId);
        Task RemoveEyeColorAsync(int eyeColorId);
    }
}
