using kennel_bambino.web.Models;
using kennel_bambino.web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface IPhotoService
    {
        Photo AddPhoto(Photo photo, IFormFile imageFile);
        Task<Photo> AddPhotoAsync(Photo photo, IFormFile imageFile);
        PhotoPagingViewModel GetPhotos(int pageNumber, int pageSize);
        Task<PhotoPagingViewModel> GetPhotosAsync(int pageNumber, int pageSize);
        Photo GetPhotoById(int photoId);
        Task<Photo> GetPhotoByIdAsync(int photoId);
        List<SelectListItem> GetPetSelectListItem();
        Task<List<SelectListItem>> GetPetSelectListItemAsync();
        Photo UpdatePhoto(Photo photo, IFormFile imageFile);
        Task<Photo> UpdatePhotoAsync(Photo photo, IFormFile imageFile);
        void RemovePhoto(int photoId);
        Task RemovePhotoAsync(int photoId);
        int PhotosCount();
        Task<int> PhotosCountAsync();
    }
}
