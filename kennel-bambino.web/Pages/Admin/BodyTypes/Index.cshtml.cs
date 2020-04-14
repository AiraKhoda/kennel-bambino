using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kennel_bambino.web.Pages.Admin.BodyTypes
{
    public class IndexModel : PageModel
    {

        private readonly IBodyTypeService _bodyTypeService;
        public IndexModel(IBodyTypeService bodyTypeService)
        {
            _bodyTypeService = bodyTypeService;
        }

        public List<BodyType> BodyTypes { get; private set; }
        public int BodyTypesCount { get; set; }

        public async Task OnGetAsync()
        {
            BodyTypes = await _bodyTypeService.GetAllBodyTypesAsync();
            BodyTypesCount = await _bodyTypeService.BodyTypesCountAsync();
        }
    }
}