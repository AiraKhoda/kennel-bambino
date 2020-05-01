using kennel_bambino.web.Data;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Services
{
    public class EyeColorService : IEyeColorService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EyeColorService> _logger;

        public EyeColorService(ApplicationDbContext context, ILogger<EyeColorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new EyeColor to database
        /// </summary>
        /// <param name="eyeColor"></param>
        /// <returns></returns>
        public EyeColor AddEyeColor(EyeColor eyeColor)
        {
            try
            {
                _context.EyeColors.Add(eyeColor);
                _context.SaveChanges();

                return eyeColor;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<EyeColor> AddEyeColorAsync(EyeColor eyeColor)
        {
            try
            {
                await _context.EyeColors.AddAsync(eyeColor);
                await _context.SaveChangesAsync();

                return eyeColor;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public int EyeColorsCount() => _context.EyeColors.Count();


        public async Task<int> EyeColorsCountAsync() => await _context.EyeColors.CountAsync();


        /// <summary>
        /// Get All EyeColors
        /// </summary>
        /// <returns></returns>
        public List<EyeColor> GetAllEyeColors() => _context.EyeColors.ToList();


        public async Task<List<EyeColor>> GetAllEyeColorsAsync() => await _context.EyeColors.ToListAsync();

        /// <summary>
        /// Get EyeColor by id from database
        /// </summary>
        /// <param name="eyeColorId"></param>
        /// <returns></returns>
        public EyeColor GetEyeColorById(int eyeColorId) => _context.EyeColors.SingleOrDefault(e => e.EyeColorId == eyeColorId);


        public async Task<EyeColor> GetEyeColorByIdAsync(int eyeColorId) => await _context.EyeColors.SingleOrDefaultAsync(e => e.EyeColorId == eyeColorId);

        public List<SelectListItem> GetEyeColorSelectList() => _context.EyeColors.Select(e => new
           SelectListItem
        {
            Text = e.Name,
            Value = e.EyeColorId.ToString()
        }).ToList();


        public async Task<List<SelectListItem>> GetEyeColorSelectListAsync() => await _context.EyeColors.Select(e => new
           SelectListItem
        {
            Text = e.Name,
            Value = e.EyeColorId.ToString()
        }).ToListAsync();


        /// <summary>
        /// Remove the EyeColor from database
        /// </summary>
        /// <param name="eyeColorId"></param>
        public void RemoveEyeColor(int eyeColorId)
        {
            var eyeColor = GetEyeColorById(eyeColorId);

            _context.EyeColors.Remove(eyeColor);
            _context.SaveChanges();
        }

        public async Task RemoveEyeColorAsync(int eyeColorId)
        {
            var eyeColor = await GetEyeColorByIdAsync(eyeColorId);

            _context.EyeColors.Remove(eyeColor);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Update the EyeColor's data from database
        /// </summary>
        /// <param name="eyeColor"></param>
        /// <returns></returns>
        public EyeColor UpdateEyeColor(EyeColor eyeColor)
        {
            try
            {
                _context.EyeColors.Update(eyeColor);
                _context.SaveChanges();

                return eyeColor;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<EyeColor> UpdateEyeColorAsync(EyeColor eyeColor)
        {
            try
            {
                _context.EyeColors.Update(eyeColor);
                await _context.SaveChangesAsync();

                return eyeColor;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }
    }
}

