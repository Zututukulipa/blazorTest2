using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ElsaWebApp.Controllers.DataAccess;
using ElsaWebApp.Models.Database;

namespace ElsaWebApp.Services.DataAccess
{
    public class GroupService
    {
        private HttpClient _http { get; set; }

        public GroupService(HttpClient httpClient)
        {
            _http = httpClient;
        }


        public async Task<bool> AddGroups(List<UserGroup> groups)
        {
            var responseMessage = await _http.PostAsJsonAsync("group/addRange", groups);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<UserGroup>> GetGroups()
        {
            return await _http.GetFromJsonAsync<List<UserGroup>>("group/getAll");
        }

        public async Task<bool> InsertUsersIntoGroups(object userGroups)
        {
            var responseMessage = await _http.PostAsJsonAsync("group/assignRange", userGroups);
            return responseMessage.IsSuccessStatusCode;
        }
    }
}