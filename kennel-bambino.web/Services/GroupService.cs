using kennel_bambino.web.Data;
using kennel_bambino.web.Helpers;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Services
{
    public class GroupService : IGroupService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<GroupService> _logger;

        public GroupService(ApplicationDbContext context, ILogger<GroupService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new Group to database
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public Group AddGroup(IFormFile imageFile, Group group)
        {
            try
            {
                group.ImageName = imageFile.UploadPhoto();

                _context.Groups.Add(group);
                _context.SaveChanges();

                return group;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Group> AddGroupAsync(IFormFile imageFile, Group group)
        {
            try
            {
                group.ImageName = imageFile.UploadPhoto();

                await _context.Groups.AddAsync(group);
                await _context.SaveChangesAsync();

                return group;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public int AllGroupsCount() => _context.Groups.Count();


        public async Task<int> AllGroupsCountAsync() => await _context.Groups.CountAsync();


        /// <summary>
        /// Get All Groups and subGroups
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Group> GetAllGroups() => _context.Groups.ToList();


        public async Task<IEnumerable<Group>> GetAllGroupsAsync() => await _context.Groups.ToListAsync();

        /// <summary>
        /// Get Group By id from database
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Group GetGroupById(int groupId) => _context.Groups.AsNoTracking().SingleOrDefault(g => g.GroupId == groupId);


        public async Task<Group> GetGroupByIdAsync(int groupId) => await _context.Groups.AsNoTracking().SingleOrDefaultAsync(g => g.GroupId == groupId);

        /// <summary>
        /// Get all Groups
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Group> GetGroups() => _context.Groups.Where(g => g.ParentId == null).ToList();


        public async Task<IEnumerable<Group>> GetGroupsAsync() => await _context.Groups.Where(g => g.ParentId == null).ToListAsync();

        public List<SelectListItem> GetGroupSelectList() => _context.Groups.Where(g => g.ParentId == null).Select(g => new
              SelectListItem
        {
            Text = g.Title,
            Value = g.GroupId.ToString()
        }).ToList();


        public async Task<List<SelectListItem>> GetGroupSelectListAsync() => await _context.Groups.Where(g => g.ParentId == null).Select(g => new
           SelectListItem
        {
            Text = g.Title,
            Value = g.GroupId.ToString()
        }).ToListAsync();



        /// <summary>
        /// Get all SubGroups
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Group> GetSubGroups() => _context.Groups.Where(g => g.ParentId != null).ToList();


        public async Task<IEnumerable<Group>> GetSubGroupsAsync() => await _context.Groups.Where(g => g.ParentId != null).ToListAsync();

        public List<SelectListItem> GetSubGroupSelectList(int groupId) => _context.Groups
            .Where(sg => sg.ParentId == groupId)
            .Select(sg => new 
            SelectListItem 
            { 
                Text = sg.Title, Value = sg.GroupId.ToString()
            }).ToList();
       
        public Task<List<SelectListItem>> GetSubGroupSelectListAsync(int groupId) => _context.Groups
            .Where(sg => sg.ParentId == groupId)
            .Select(sg => new
            SelectListItem
            {
                Text = sg.Title,
                Value = sg.GroupId.ToString()
            }).ToListAsync();

        public int GroupsCount() => _context.Groups.Where(g => g.ParentId == null).Count();



        public async Task<int> GroupsCountAsync() => await _context.Groups.Where(g => g.ParentId == null).CountAsync();


        /// <summary>
        /// Remove the Group from database
        /// </summary>
        /// <param name="groupId"></param>
        public void RemoveGroup(int groupId)
        {
            var group = GetGroupById(groupId);

            _context.Groups.Remove(group);
            _context.SaveChanges();
        }

        public async Task RemoveGroupAsync(int groupId)
        {
            var group = GetGroupById(groupId);

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }

        public int SubGroupsCount() => _context.Groups.Where(sg => sg.ParentId != null).Count();


        public async Task<int> SubGroupsCountAsync() => await _context.Groups.Where(sg => sg.ParentId != null).CountAsync();


        /// <summary>
        /// Update the group's data from database
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public Group UpdateGroup(IFormFile imageFile, Group group)
        {
            try
            {
                group.ImageName = imageFile.UpdateUploadedGroupPhoto(group);

                _context.Groups.Add(group);
                _context.SaveChanges();

                return group;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Group> UpdateGroupAsync(IFormFile imageFile, Group group)
        {
            try
            {
                group.ImageName = imageFile.UpdateUploadedGroupPhoto(group);

                await _context.Groups.AddAsync(group);
                await _context.SaveChangesAsync();

                return group;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }
    }
}
