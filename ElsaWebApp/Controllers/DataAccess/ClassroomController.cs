using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElsaWebApp.Controllers.DataAccess
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomController
    {
        private SchooldContext Context { get; }

        public ClassroomController(SchooldContext context)
        {
            Context = context;
        }
        
          [HttpGet("GetAll")]
        public async Task<ActionResult<List<Classroom>>> GetClassrooms()
        {
            var classrooms = await Context.Classrooms.ToListAsync();
            if (classrooms.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(classrooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Classroom>> GetClassroomById(int id)
        {
            var classroom = await Context.Classrooms.FirstOrDefaultAsync(u => u.ClassroomId == id);
            if (classroom == null)
                return new NotFoundResult();
            return new OkObjectResult(classroom);
        }
        

        [HttpPost]
        public async Task<ActionResult> InsertClassroom(Classroom classroom)
        {
            try
            {
                await Context.Classrooms.AddAsync(classroom);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("addRange")]
        public async Task<ActionResult> InsertClassroom(List<Classroom> classrooms)
        {
            try
            {
                await Context.Classrooms.AddRangeAsync(classrooms);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateClassroom(Classroom classroom)
        {
            try
            {
                await Task.Run(() => { Context.Update(classroom); });
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }
    }
}