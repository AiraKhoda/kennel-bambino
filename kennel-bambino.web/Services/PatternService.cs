using kennel_bambino.web.Data;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Services
{
    public class PatternService : IPatternService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PatternService> _logger;

        public PatternService(ApplicationDbContext context, ILogger<PatternService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new Pattern to database
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public Pattern AddPattern(Pattern pattern)
        {
            try
            {
                _context.Patterns.Add(pattern);
                _context.SaveChanges();

                return pattern;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Pattern> AddPatternAsync(Pattern pattern)
        {
            try
            {
                await _context.Patterns.AddAsync(pattern);
                await _context.SaveChangesAsync();

                return pattern;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }
        /// <summary>
        /// Get All Patterns
        /// </summary>
        /// <returns></returns>
        public List<Pattern> GetAllPatterns() => _context.Patterns.ToList();


        public async Task<List<Pattern>> GetAllPatternsAsync() => await _context.Patterns.ToListAsync();

        /// <summary>
        /// Get Pattern by id from database
        /// </summary>
        /// <param name="eyeColorId"></param>
        /// <returns></returns>
        public Pattern GetPatternById(int patternId) => _context.Patterns.SingleOrDefault(p => p.PatternId == patternId);


        public async Task<Pattern> GetPatternByIdAsync(int patternId) => await _context.Patterns.SingleOrDefaultAsync(p => p.PatternId == patternId);

        public int PatternsCount() => _context.Patterns.Count();


        public async Task<int> PatternsCountAsync() => await _context.Patterns.CountAsync();


        /// <summary>
        /// Remove the Pattern from database
        /// </summary>
        /// <param name="patternId"></param>
        public void RemovePattern(int patternId)
        {
            var pattern = GetPatternById(patternId);

            _context.Patterns.Remove(pattern);
            _context.SaveChanges();
        }

        public async Task RemovePatternAsync(int patternId)
        {
            var pattern = await GetPatternByIdAsync(patternId);

            _context.Patterns.Remove(pattern);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Update the Pattern's data from database
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public Pattern UpdatePattern(Pattern pattern)
        {
            try
            {
                _context.Patterns.Update(pattern);
                _context.SaveChanges();

                return pattern;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Pattern> UpdatePatternAsync(Pattern pattern)
        {
            try
            {
                _context.Patterns.Update(pattern);
                await _context.SaveChangesAsync();

                return pattern;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }
    }
}

