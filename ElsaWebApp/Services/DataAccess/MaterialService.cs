using System;
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
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _http = httpClient;
            _http.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<bool> AddMaterial(SupportMaterial material)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/supportMaterial/add", material);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<SupportMaterial>> GetMaterials()
        {
            return await _http.GetFromJsonAsync<List<SupportMaterial>>("api/supportMaterial/GetAll");
        }
        
        
    }
}