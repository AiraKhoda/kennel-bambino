using kennel_bambino.web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface IGroupService
    {
        Group AddGroup(IFormFile imageFile, Group group);
        Task<Group> AddGroupAsync(IFormFile imageFile, Group group);
        IEnumerable<Group> GetAllGroups();
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        IEnumerable<Group> GetGroups();
        Task<IEnumerable<Group>> GetGroupsAsync();
        IEnumerable<Group> GetSubGroups();
        Task<IEnumerable<Group>> GetSubGroupsAsync();
        Group GetGroupById(int groupId);
        Task<Group> GetGroupByIdAsync(int groupId);
        Group UpdateGroup(IFormFile imageFile, Group group);
        Task<Group> UpdateGroupAsync(IFormFile imageFile, Group group);
        void RemoveGroup(int groupId);
        Task RemoveGroupAsync(int groupId);
        int AllGroupsCount();
        Task<int> AllGroupsCountAsync();
    }
}
