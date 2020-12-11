using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Services.DataAccess;

namespace DataFiller
{
    public class GroupAdd
    {
        private readonly GroupService _service;
        private readonly UserService _userService;
        private readonly Random _random;

        public GroupAdd(HttpClient httpClient)
        {
            _random = new Random();
            _service = new GroupService(httpClient);
            _userService = new UserService(httpClient);
        }
        
        public async Task<bool> AddGroups(int amount)
        {
            List<UserGroup> groups = new List<UserGroup>();
            var users = await _userService.GetUsers();
            var professors = users.FindAll(usr => usr.RoleId == 2);
            for (int i = 0; i < amount; i++)
            {
                var group = new UserGroup{GroupName = $"G{i}", LeaderId = professors[_random.Next(professors.Count)].UserId};
                groups.Add(group);
            }

            bool responseStatus = await _service.AddGroups(groups);

            return responseStatus;
        }
        
        public async Task<bool> FillGroups()
        {
            List<UserGroup> groups = await _service.GetGroups();
            var users = await _userService.GetUsers();
            var students = users.FindAll(usr => usr.RoleId == 3);
            List<UserGroups> studentGroups = new List<UserGroups>();
            foreach (var group in groups)
            {
                for (int i = 0; i < _random.Next(20); i++)
                {
                    UserGroups userGroup = new UserGroups(){
                        GroupId = group.GroupId, 
                        UserId = students[_random.Next(students.Count)].UserId
                    };
                
                    if(!studentGroups.Contains(userGroup))
                        studentGroups.Add(userGroup);
                }
                
            }
            bool responseStatus = await _service.InsertUsersIntoGroups(studentGroups);

            return responseStatus;
        }
        
       
    }

   
}