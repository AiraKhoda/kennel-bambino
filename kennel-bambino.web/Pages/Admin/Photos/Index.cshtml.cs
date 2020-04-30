using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kennel_bambino.web.Pages.Admin.Photos
{
    public class IndexModel : PageModel
    {

        private readonly IPhotoService _photoService;
        public IndexModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        
        public PhotoPagingViewModel PhotoPagingVM { get; private set; }
        public int PhotosCount { get; private set; }

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 32)
        {
            PhotoPagingVM = await _photoService.GetPhotosAsync(pageNumber, pageSize);
            PhotosCount = await _photoService.PhotosCountAsync();
        }
    }
}