using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Services.DataAccess;

namespace DataFiller
{
    public class MaterialAdd
    {
        private readonly MaterialService _service;

        public MaterialAdd(HttpClient httpClient)
        {
            _service = new MaterialService(httpClient);
        }
        public async Task<bool> AddMaterial()
        {
            var path = @"/home/zututukulipa/Desktop/barcode.txt";
            var file = File.Open(path, FileMode.Open);
            var data = new byte[file.Length];
            file.Read(data, 0, (int) file.Length);
            file.Close();
            SupportMaterial sm = new SupportMaterial
            {
                BinaryData = data,
                MaterialName = "TEST FILE",
                MaterialCreated = DateTime.Now,
                SubjectId = 59,
                PageCount = 1
            };
            
            return await _service.AddMaterial(sm);
        }
        
        public async Task<List<SupportMaterial>> GetMaterials()
        {
            var materials = await _service.GetMaterials();

            return materials;
        }
    }
    
}