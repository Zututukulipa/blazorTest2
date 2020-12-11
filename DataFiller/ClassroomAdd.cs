using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Services.DataAccess;

namespace DataFiller
{
    public class ClassroomAdd
    {
        private readonly ClassroomService _service = new ClassroomService(new HttpClient());
        private Random _random = new Random();

        public async Task<bool> AddClassrooms(int amount)
        {
            var classrooms = await GenerateClassrooms(amount);
            return await _service.InsertClassroom(classrooms);
        }

        private Task<List<Classroom>> GenerateClassrooms(int amount)
        {
            List<Classroom> classrooms = new List<Classroom>();
            for (int i = 0; i < amount; i++)
            {
                var room = new Classroom()
                    {ClassroomName = $"C{i}", ClassroomCapacity = _random.Next(10, 20), IsOperational = true};
                classrooms.Add(room);
            }

            return Task.FromResult(classrooms);
        }
    }
}