using kennel_bambino.web.Data;
using kennel_bambino.web.Helpers;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using kennel_bambino.web.ViewModels;
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
    public class PetService : IPetService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PetService> _logger;

        public object PetId { get; private set; }

        public PetService(ApplicationDbContext context, ILogger<PetService> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add new pet to database
        /// </summary>
        /// <param name="pet"></param>
        /// <param name="petPhoto"></param>
        /// <returns></returns>
        public Pet AddPEet(Pet pet, List<IFormFile> petPhoto)
        {
            try
            {
                _context.Pets.Add(pet);
                _context.SaveChanges();



                foreach (var imageName in UploadPetImages(petPhoto))
                {
                    _context.Photos.Add(new Photo
                    {
                        PetId = pet.PetId,
                        PhotoName = imageName
                    }
                    );

                    _context.SaveChanges();
                }

                return pet;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }


        public async Task<Pet> AddPetAsync(Pet pet, List<IFormFile> petPhoto)
        {
            try
            {
                await _context.Pets.AddAsync(pet);
                await _context.SaveChangesAsync();



                foreach (var imageName in UploadPetImages(petPhoto))
                {
                    await _context.Photos.AddAsync(new Photo
                    {
                        PetId = pet.PetId,
                        PhotoName = imageName
                    }
                      );

                    await _context.SaveChangesAsync();
                }

                return pet;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }
        /// <summary>
        /// Get all Pets
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PetPagingViewModel GetAllPets(int pageNumber = 1, int pageSize = 32)
        {
            IQueryable<Pet> pets = _context.Pets;

            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int petsCount = pets.Count();

            int pageCount = (int)Math.Ceiling(decimal.Divide(petsCount, take));

            return new PetPagingViewModel
            {
                Pets = pets.Skip(skip)
                .Take(take)
                .Where(p => p.IsDelete == false)
                .OrderByDescending(p => p.PetId)
                .ToList(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }

        public async Task<PetPagingViewModel> GetAllPetsAsync(int pageNumber, int pageSize)
        {
            IQueryable<Pet> pets = _context.Pets;

            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int petsCount = await pets.CountAsync();

            int pageCount = (int)Math.Ceiling(decimal.Divide(petsCount, take));

            return new PetPagingViewModel
            {
                Pets = await pets.Skip(skip)
                .Take(take)
                .Where(p => p.IsDelete == false)
                .OrderByDescending(p => p.PetId)
                .ToListAsync(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }
        /// <summary>
        /// Get pet ById from database
        /// </summary>
        /// <param name="petId"></param>
        /// <returns></returns>
        public Pet GetPetById(int petId) => _context.Pets.SingleOrDefault(p => p.PetId == petId);


        public async Task<Pet> GetPetByIdAsync(int petId) => await _context.Pets.SingleOrDefaultAsync(p => p.PetId == petId);


        public int PetCount() => _context.Pets.Where(p=>p.IsDelete==false).Count();


        public async Task<int> PetCountAsync() => await _context.Pets.Where(p => p.IsDelete == false).CountAsync();

        /// <summary>
        /// Remove the pet from database
        /// </summary>
        /// <param name="petId"></param>
        public void RemovePet(int petId)
        {
            RemovePetImages(petId);

            var pet = GetPetById(petId);
            pet.IsDelete = true;

            _context.Pets.Update(pet);
            _context.SaveChanges();
        }

        public async Task RemovePetAsync(int petId)
        {
            RemovePetImages(petId);

            var pet = await GetPetByIdAsync(petId);
            pet.IsDelete = true;

            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Update the pet's from database 
        /// </summary>
        /// <param name="pet"></param>
        /// <param name="petPhoto"></param>
        /// <returns></returns>
        public Pet UpdatePet(Pet pet, List<IFormFile> petPhoto)
        {
            try
            {
                _context.Pets.Update(pet);
                _context.SaveChanges();
                if (petPhoto != null)
                {
                    foreach (var imageName in UploadPetImages(petPhoto))
                    {
                        _context.Photos.Add(new Photo
                        {
                            PetId = pet.PetId,
                            PhotoName = imageName
                        }
                   );

                        _context.SaveChanges();
                    }
                }
                return pet;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public PetPagingViewModel SearchPets(string code, string title, int pageSize = 30, int pageNumber = 1)
        {
            IQueryable<Pet> pets = _context.Pets.Where(p => p.Title.TextTransform().Contains(title.TextTransform())
            || p.Code.TextTransform().Contains(code.TextTransform()));


            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int petsCount = pets.Count();

            int pageCount = (int)Math.Ceiling(decimal.Divide(petsCount, take));

            return new PetPagingViewModel
            {
                Pets = pets.Skip(skip)
                .Take(take)
                .Where(p => p.IsDelete == false)
                .OrderByDescending(p => p.PetId)
                .ToList(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };

        }

        public async Task<PetPagingViewModel> SearchPetsAsync(string code, string title, int pageSize = 30, int pageNumber = 1)
        {
            IQueryable<Pet> pets = _context.Pets.Where(p => p.Title.TextTransform().Contains(title.TextTransform())
          || p.Code.TextTransform().Contains(code.TextTransform()));


            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int petsCount = await pets.CountAsync();

            int pageCount = (int)Math.Ceiling(decimal.Divide(petsCount, take));

            return new PetPagingViewModel
            {
                Pets = await pets.Skip(skip)
                .Take(take)
                .Where(p => p.IsDelete == false)
                .OrderByDescending(p => p.PetId)
                .ToListAsync(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };

        }

        public async Task<Pet> UpdatePetAsync(Pet pet, List<IFormFile> petPhoto)
        {
            try
            {
                _context.Pets.Update(pet);
                await _context.SaveChangesAsync();
                if (petPhoto != null)
                {
                    foreach (var imageName in UploadPetImages(petPhoto))
                    {
                        await _context.Photos.AddAsync(new Photo
                        {
                            PetId = pet.PetId,
                            PhotoName = imageName
                        }
                    );

                        await _context.SaveChangesAsync();
                    }
                }
                return pet;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        #region Helper

        private static List<string> UploadPetImages(List<IFormFile> petImages)
        {
            if (petImages != null)
            {
                List<string> petImagesList = new List<string>();
                foreach (var petImage in petImages)
                {
                    string petImageName = Guid.NewGuid().ToString() + Path.GetExtension(petImage.FileName);
                    string petPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/thumbnails/", petImageName);

                    using (var fileStream = new FileStream(petPath, FileMode.Create))
                    {
                        petImage.CopyTo(fileStream);
                    }

                    petImagesList.Add(petImageName);
                }
                return petImagesList;
            }

            return new List<string> { "deafult.jpg" };
        }

        public void RemovePetImages(int petId)
        {
            var petOldImages = _context.Photos.Where(p => p.PetId == petId);

            if (petOldImages.Any())
            {
                foreach (var petImage in petOldImages.Select(p => p.PhotoName))
                {
                    string petPath = Path.Combine(Directory.GetCurrentDirectory(), $"/wwwroot/images/thumbnails/", petImage);

                    if (new FileInfo(petPath).Exists)
                    {
                        new FileInfo(petImage).Delete();
                    }
                }

                foreach (var oldPetImages in petOldImages)
                {
                    _context.Photos.Remove(oldPetImages);
                }

                _context.SaveChanges();
            }
        }

        #endregion
    }
}
