using kennel_bambino.web.Models;
using kennel_bambino.web.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface IPetService
    {
        Pet AddPet(Pet pet, List<IFormFile> petPhoto);
        Task<Pet> AddPetAsync(Pet pet, List<IFormFile> petPhoto);
        PetPagingViewModel GetAllPets(int pageNumber, int pageSize);
        Task<PetPagingViewModel> GetAllPetsAsync(int pageNumber, int pageSize);
        Pet GetPetById(int petId);
        Task<Pet> GetPetByIdAsync(int petId);
        Pet UpdatePet(Pet pet, List<IFormFile> petPhoto);
        Task<Pet> UpdatePetAsync(Pet pet, List<IFormFile> petPhoto);
        void RemovePet(int petId);
        Task RemovePetAsync(int petId);
        PetPagingViewModel SearchPets(string code, string title, int pageSize, int pageNumber);
        Task<PetPagingViewModel> SearchPetsAsync(string code, string title, int pageSize, int pageNumber);
        int PetCount();
        Task<int> PetCountAsync();
    }
}
