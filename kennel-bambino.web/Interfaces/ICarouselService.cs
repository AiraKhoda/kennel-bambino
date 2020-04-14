using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface ICarouselService
    {
        Carousel AddCarousel(Carousel carousel,IFormFile carouselfile);
        Task<Carousel> AddCarouselAsync(Carousel carousel, IFormFile carouselfile);
        IEnumerable<Carousel> GetCarousels();
        Task<IEnumerable<Carousel>> GetCarouselsAsync();
        Carousel GetCarouselById(int carouselId);
        Task<Carousel> GetCarouselByIdAsync(int carouselId);
        Carousel UpdateCarousel(Carousel carousel,IFormFile carouselFile);
        Task<Carousel> UpdateCarouselAsync(Carousel carousel, IFormFile carouselFile);
        void RemoveCarousel(int carouselId);
        Task RemoveCarouselAsync(int carouselId);
        int GetCarouselsCount();
        Task<int> GetCarouselsCountAsync();

    }
}
