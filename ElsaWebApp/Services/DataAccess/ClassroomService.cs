using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;

namespace ElsaWebApp.Services.DataAccess
{
    public class ClassroomService
    {
        private HttpClient _http;
        public ClassroomService(HttpClient client)
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }
            };

            _http = client;
            _http.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<List<Classroom>> GetClassrooms()
        {
            var users = await _http.GetFromJsonAsync<List<Classroom>>("api/classroom/GetAll");
            return users;
        }

        public async Task<Classroom> GetClassroomById(int id)
        {
            var user = await _http.GetFromJsonAsync<Classroom>($"api/classroom/{id}");
            return user;
        }

        public async Task<bool> InsertClassroom(Classroom classroom)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/classroom", classroom);
            return responseMessage.IsSuccessStatusCode;
        }
        
        public async Task<bool> InsertClassroom(List<Classroom> classrooms)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/classroom/addrange", classrooms);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClassroom(Classroom classroom)
        {
            var responseMessage = await _http.PutAsJsonAsync("api/classroom", classroom);
            return responseMessage.IsSuccessStatusCode;
        }
    }
}