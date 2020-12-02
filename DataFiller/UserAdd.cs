using System;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Controllers.DataAccess;
using ElsaWebApp.Models.Database;
using HumanLab;

namespace DataFiller
{
    public class UserAdd
    {
        private readonly PersonLib _lib = new PersonLib();
        private readonly UserService _service = new UserService(new HttpClient());
        public async Task<bool> AddUsers(int amount)
        {
            var users = await _lib.GetPeople(50);
            
            return await _service.InsertUser(users.ConvertAll(new Converter<Person, DbUser>(Mapper.GetUser)));
        }

    }
}