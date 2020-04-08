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

        public Carousel GetCarouselById(Carousel carouselId)
        {
            throw new NotImplementedException();
        }

        public Task<Carousel> GetCarouselByIdAsync(Carousel carouselId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get all Carousels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Carousel> GetCarousels()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Carousel>> GetCarouselsAsync()
        {
            throw new NotImplementedException();
        }

        public void RemoveCarousel(int carouselId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCarouselAsync(int carouselId)
        {
            throw new NotImplementedException();
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
    }
}
