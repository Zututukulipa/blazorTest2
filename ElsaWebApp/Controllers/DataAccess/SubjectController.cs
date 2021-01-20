using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ElsaWebApp.Controllers.DataAccess
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController
    {
        private SchooldContext Context { get; }

        public SubjectController(SchooldContext context)
        {
            Context = context;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<Subject>>> GetSubjects()
        {
            var subjects = await Context.Subjects.ToListAsync();
            if (subjects.Count == 0)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(subjects);
        }
        
        [HttpGet("getAllRead")]
        public async Task<ActionResult<List<Subject>>> GetSubjectsRead()
        {
            var subjects = await Context.Subjects.AsNoTracking().ToListAsync();
            if (subjects.Count == 0)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(subjects);
        }

        [HttpGet("{id}")]
        public ActionResult<Task<Subject>> GetSubject(int id)
        {
            var list = Context.Subjects.FirstOrDefaultAsync(subject => subject.SubjectId == id);
            if (list != null)
            {
                return new OkObjectResult(list);
            }

            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<ActionResult> InsertSubject(Subject subject)
        {
            try
            {
                await Context.Subjects.AddAsync(subject);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("addRange")]
        public async Task<ActionResult> InsertSubject(List<Subject> subjects)
        {
            try
            {
                await Context.Subjects.AddRangeAsync(subjects);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("pairSubjectStudentRange")]
        public async Task<ActionResult> InsertSubject(List<UserSubjects> pairs)
        {
            try
            {
                await Context.UserSubjects.AddRangeAsync(pairs);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("pairSubject")]
        public async Task<ActionResult> PairSubject(ClassroomSubjects pair)
        {
            try
            {
                await Context.ClassroomSubjects.AddAsync(pair);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("pairSubjectRange")]
        public async Task<ActionResult> PairSubject(List<ClassroomSubjects> pairs)
        {
            try
            {
                foreach (var pair in pairs)
                {
                    await Context.ClassroomSubjects.AddAsync(pair);
                }

                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSubject(Subject subject)
        {
            try
            {
                await Task.Run(() => { Context.Update(subject); });
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