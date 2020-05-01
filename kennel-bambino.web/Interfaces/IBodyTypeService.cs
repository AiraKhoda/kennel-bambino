using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface IBodyTypeService
    {
        BodyType AddBodyType(BodyType bodyType);
        Task<BodyType> AddBodyTypeAsync(BodyType bodyType);
        List<BodyType> GetAllBodyTypes();
        Task<List<BodyType>> GetAllBodyTypesAsync();
        List<SelectListItem> GetBodyTypeSelectList();
        Task<List<SelectListItem>> GetBodyTypeSelectListAsync();
        BodyType GetBodyTypeById(int bodyTypeId);
        Task<BodyType> GetBodyTypeByIdAsync(int bodyTypeId);
        BodyType UpdateBodyType(BodyType bodyType);
        Task<BodyType> UpdateBodyTypeAsync(BodyType bodyType);
        void RemoveBodyType(int bodyTypeId);
        Task RemoveBodyTypeAsync(int bodyTypeId);
        int BodyTypesCount();
        Task<int> BodyTypesCountAsync();

    }
}
