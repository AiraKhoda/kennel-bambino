using kennel_bambino.web.Data;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Services
{
    public class BodyTypeService : IBodyTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BodyTypeService> _logger;

        public BodyTypeService(ApplicationDbContext context, ILogger<BodyTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new BodyType to database
        /// </summary>
        /// <param name="bodyType"></param>
        /// <returns></returns>
        public BodyType AddBodyType(BodyType bodyType)
        {
            try
            {
                _context.BodyTypes.Add(bodyType);
                _context.SaveChanges();

                return bodyType;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<BodyType> AddBodyTypeAsync(BodyType bodyType)
        {
            try
            {
                await _context.BodyTypes.AddAsync(bodyType);
                await _context.SaveChangesAsync();

                return bodyType;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public int BodyTypesount() => _context.BodyTypes.Count();


        public async Task<int> BodyTypesCountAsync() => await _context.BodyTypes.CountAsync();
       

        

        /// <summary>
        /// Get All BodyTypes
        /// </summary>
        /// <returns></returns>
        public List<BodyType> GetAllBodyTypes() => _context.BodyTypes.ToList();


        public async Task<List<BodyType>> GetAllBodyTypesAsync() => await _context.BodyTypes.ToListAsync();

        /// <summary>
        /// Get BodyType by id from database
        /// </summary>
        /// <param name="bodyTypeId"></param>
        /// <returns></returns>
        public BodyType GetBodyTypeById(int bodyTypeId) => _context.BodyTypes.SingleOrDefault(b => b.BodyTypeId == bodyTypeId);


        public async Task<BodyType> GetBodyTypeByIdAsync(int bodyTypeId) => await _context.BodyTypes.SingleOrDefaultAsync(b => b.BodyTypeId == bodyTypeId);

        /// <summary>
        /// Remove the BodyType from database
        /// </summary>
        /// <param name="bodyTypeId"></param>
        public void RemoveBodyType(int bodyTypeId)
        {
            var bodyType = GetBodyTypeById(bodyTypeId);

            _context.BodyTypes.Remove(bodyType);
            _context.SaveChanges();
        }

        public async Task RemoveBodyTypeAsync(int bodyTypeId)
        {
            var bodyType = await GetBodyTypeByIdAsync(bodyTypeId);

            _context.BodyTypes.Remove(bodyType);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Update the BodyType's data from database
        /// </summary>
        /// <param name="bodyType"></param>
        /// <returns></returns>
        public BodyType UpdateBodyType(BodyType bodyType)
        {
            try
            {
                _context.BodyTypes.Update(bodyType);
                _context.SaveChanges();

                return bodyType;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<BodyType> UpdateBodyTypeAsync(BodyType bodyType)
        {
            try
            {
                _context.BodyTypes.Update(bodyType);
                await _context.SaveChangesAsync();

                return bodyType;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }
    }
}
