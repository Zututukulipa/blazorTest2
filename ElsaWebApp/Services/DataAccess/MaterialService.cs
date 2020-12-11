using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;

namespace ElsaWebApp.Services.DataAccess
{
    public class MaterialService
    {
        private HttpClient _http;
        public MaterialService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<bool> AddMaterial(SupportMaterial material)
        {
            var responseMessage = await _http.PostAsJsonAsync<SupportMaterial>("supportMaterial", material);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<SupportMaterial>> GetMaterials()
        {
            return await _http.GetFromJsonAsync<List<SupportMaterial>>("supportMaterial/GetAll");
        }
        
        
    }
}