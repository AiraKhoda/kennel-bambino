using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface IPhotoService
    {
        Photo AddPhoto(Photo photo, IFormFile imageFile);
        Task<Photo> AddPhotoAsync(Photo photo, IFormFile imageFile);
    }
}
