using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;

namespace ElsaWebApp.Controllers.DataAccess
{
    public class UserService
    {
        private HttpClient _http;
        public UserService(HttpClient client)
        {
            _http = client;
            _http.BaseAddress = new Uri("https://localhost:8080/");
        }

        public async Task<List<DbUser>> GetUsers()
        {
            var users = await _http.GetFromJsonAsync<List<DbUser>>("user/GetAll");
            return users;
        }

        public async Task<DbUser> GetUserById(int id)
        {
            var user = await _http.GetFromJsonAsync<DbUser>($"user/{id}");
            return user;
        }

        public async Task<bool> InsertUser(DbUser user)
        {
            var responseMessage = await _http.PostAsJsonAsync("user", user);
            return responseMessage.IsSuccessStatusCode;
        }
        
        public async Task<bool> InsertStudent(DbUser user)
        {
            user.RoleId = 3;
            user.StudyYear = "1";
            var responseMessage = await _http.PostAsJsonAsync("user", user);
            return responseMessage.IsSuccessStatusCode;
        }
        
        public async Task<bool> InsertUser(List<DbUser> users)
        {
            var responseMessage = await _http.PostAsJsonAsync("user", users);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUser(DbUser user)
        {
            var responseMessage = await _http.PutAsJsonAsync("user", user);
            return responseMessage.IsSuccessStatusCode;
        }
    }
}