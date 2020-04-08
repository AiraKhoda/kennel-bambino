using kennel_bambino.web.Data;
using kennel_bambino.web.Helpers;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    }
}
