using kennel_bambino.web.Data;
using kennel_bambino.web.Helpers;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Services
{
    public class CarouselService : ICarouselService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarouselService> _logger;

        public CarouselService(ApplicationDbContext context, ILogger<CarouselService> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add new Carousel to database
        /// </summary>
        /// <param name="carousel"></param>
        /// <param name="carouselfile"></param>
        /// <returns></returns>
        public Carousel AddCarousel(Carousel carousel, IFormFile carouselfile)
        {
            try
            {
                carousel.ImageName = carouselfile.UploadPhoto("carousels");

                _context.Carousels.Add(carousel);
                _context.SaveChanges();

                return carousel;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Carousel> AddCarouselAsync(Carousel carousel, IFormFile carouselfile)
        {
            try
            {
                carousel.ImageName = carouselfile.UploadPhoto("carousels");

                await _context.Carousels.AddAsync(carousel);
                await _context.SaveChangesAsync();

                return carousel;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public Carousel GetCarouselById(int carouselId) => _context.Carousels.SingleOrDefault(c => c.CarouselId == carouselId);


        public async Task<Carousel> GetCarouselByIdAsync(int carouselId) => await _context.Carousels.SingleOrDefaultAsync(c => c.CarouselId == carouselId);

        /// <summary>
        /// Get all Carousels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Carousel> GetCarousels() => _context.Carousels.ToList();


        public async Task<IEnumerable<Carousel>> GetCarouselsAsync() => await _context.Carousels.ToListAsync();


        public int GetCarouselsCount() => _context.Carousels.Count();


        public async Task<int> GetCarouselsCountAsync() => await _context.Carousels.CountAsync();


        public void RemoveCarousel(int carouselId)
        {
            var Carousel = GetCarouselById(carouselId);

            RemoveDeletedCarousel(Carousel);

            _context.Carousels.Remove(Carousel);
            _context.SaveChanges();
        }

        public async Task RemoveCarouselAsync(int carouselId)
        {
            var Carousel = await GetCarouselByIdAsync(carouselId);

            RemoveDeletedCarousel(Carousel);

            _context.Carousels.Remove(Carousel);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Update the Carousel's data from database
        /// </summary>
        /// <param name="carousel"></param>
        /// <param name="carouselFile"></param>
        /// <returns></returns>
        public Carousel UpdateCarousel(Carousel carousel, IFormFile carouselFile)
        {
            try
            {
                carousel.ImageName = carouselFile.UpdateUploadedCarouselPhoto(carousel);

                _context.Carousels.Update(carousel);
                _context.SaveChanges();

                return carousel;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Carousel> UpdateCarouselAsync(Carousel carousel, IFormFile carouselFile)
        {
            try
            {
                carousel.ImageName = carouselFile.UpdateUploadedCarouselPhoto(carousel);

                _context.Carousels.Update(carousel);
                await _context.SaveChangesAsync();

                return carousel;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        #region Helper
        private void RemoveDeletedCarousel(Carousel carousel)
        {
            string carouselDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/carousels/", carousel.ImageName);

            if (File.Exists(carouselDeletePath))
            {
                File.Delete(carouselDeletePath);
            }
        }
        #endregion

    }
}
