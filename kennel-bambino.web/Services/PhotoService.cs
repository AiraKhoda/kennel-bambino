using kennel_bambino.web.Data;
using kennel_bambino.web.Helpers;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using kennel_bambino.web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Services
{
    public class PhotoService : IPhotoService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<PhotoService> _logger;

        public PhotoService(ApplicationDbContext context, ILogger<PhotoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new Photo to database
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        public Photo AddPhoto(Photo photo, IFormFile imageFile)
        {
            try
            {
                photo.PhotoName = imageFile.UploadPhoto();

                _context.Photos.Add(photo);
                _context.SaveChanges();

                return photo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Photo> AddPhotoAsync(Photo photo, IFormFile imageFile)
        {
            try
            {
                photo.PhotoName = imageFile.UploadPhoto();

                await _context.Photos.AddAsync(photo);
                await _context.SaveChangesAsync();

                return photo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public List<SelectListItem> GetPetSelectListItem() => _context.Pets.Select(p => new SelectListItem
        {
            Text = p.Title,
            Value = p.PetId.ToString()
        }).ToList();


        public async Task<List<SelectListItem>> GetPetSelectListItemAsync() => await _context.Pets.Select(p => new SelectListItem
        {
            Text = p.Title,
            Value = p.PetId.ToString()
        }).ToListAsync();


        /// <summary>
        /// Get photos by id from database
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public Photo GetPhotoById(int photoId) => _context.Photos.Include(p => p.Pet).SingleOrDefault(p => p.PhotoId == photoId);

        
        public async Task<Photo> GetPhotoByIdAsync(int photoId) => await _context.Photos.Include(p => p.Pet).SingleOrDefaultAsync(p => p.PhotoId == photoId);
        /// <summary>
        /// Get all Photos
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PhotoPagingViewModel GetPhotos(int pageNumber = 1, int pageSize = 20)
        {
            IQueryable<Photo> photos = _context.Photos;

            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int photosCount = photos.Count();

            int pageCount = (int)Math.Ceiling(decimal.Divide(photosCount, take));

            return new PhotoPagingViewModel
            {
                Photos = photos.Skip(skip)
                .Take(take)
                .Include(p=>p.Pet)
                .OrderByDescending(p => p.PhotoId)
                .ToList(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }

        public async Task<PhotoPagingViewModel> GetPhotosAsync(int pageNumber = 1, int pageSize = 20)
        {
            IQueryable<Photo> photos = _context.Photos;

            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int photosCount = await photos.CountAsync();

            int pageCount = (int)Math.Ceiling(decimal.Divide(photosCount, take));

            return new PhotoPagingViewModel
            {
                Photos = await photos.Skip(skip)
                .Take(take)
                .Include(p => p.Pet)
                .OrderByDescending(p => p.PhotoId)
                .ToListAsync(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }
        /// <summary>
        /// Get photos Count
        /// </summary>
        /// <returns></returns>
        public int PhotosCount() => _context.Contacts.Count();

        public async Task<int> PhotosCountAsync() => await _context.Contacts.CountAsync();

        /// <summary>
        /// Remove the photo from database
        /// </summary>
        /// <param name="photoId"></param>
        public void RemovePhoto(int photoId)
        {
            var Photo = GetPhotoById(photoId);

            RemovePhotoImage(Photo);


            _context.Photos.Remove(Photo);
            _context.SaveChanges();
        }

        public async Task RemovePhotoAsync(int photoId)
        {
            var Photo = GetPhotoById(photoId);

            RemovePhotoImage(Photo);


            _context.Photos.Remove(Photo);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update the photo from database
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        public Photo UpdatePhoto(Photo photo, IFormFile imageFile)
        {
            try
            {
                photo.PhotoName = imageFile.UpdateUploadedPetPhoto(photo);

                _context.Update(photo);
                _context.SaveChanges();

                return photo;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Photo> UpdatePhotoAsync(Photo photo, IFormFile imageFile)
        {
            try
            {
                photo.PhotoName = imageFile.UpdateUploadedPetPhoto(photo);

                _context.Update(photo);
                await _context.SaveChangesAsync();

                return photo;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        #region Helper
        private void RemovePhotoImage(Photo photo)
        {
            string photodeletedPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/thumbnails/", photo.PhotoName);

            if (File.Exists(photodeletedPath))
            {
                File.Delete(photodeletedPath);
            }
        }
        #endregion
    }
}
