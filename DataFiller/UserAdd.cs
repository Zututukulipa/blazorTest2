using System;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Services.DataAccess;
using HumanLab;

namespace DataFiller
{
    public class UserAdd
    {
        private readonly PersonLib _lib = new PersonLib();
        private readonly UserService _service = new UserService(new HttpClient());
        public async Task<bool> AddUsers(int amount)
        {
            var users = await _lib.GetPeople(amount);
            
            return await _service.InsertUser(users.ConvertAll(Mapper.GetUser));
        }
        
        public async Task<bool> AddProfessors(int amount)
        {
            var users = await _lib.GetPeople(amount);
            
            return await _service.InsertUser(users.ConvertAll(Mapper.GetProfessor));
        }

    }
}