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
    public class ExamController
    {
        
        private SchooldContext Context { get; }

        public ExamController(SchooldContext context)
        {
            Context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Exam>>> GetExams()
        {
            var exams = await Context.Exams.ToListAsync();
            if(exams.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(exams);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExamById(int id)
        {
            var exam = await Context.Exams.FirstOrDefaultAsync(u => u.ExamId == id);
            if(exam == null)
                return new NotFoundResult();
            return new OkObjectResult(exam);
        }

        [HttpPost]
        public async Task<ActionResult> InsertExam(Exam exam)
        {
            try
            {
                await Context.Exams.AddAsync(exam);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateExam(Exam exam)
        {
            try
            {
                await Task.Run(() => { Context.Update(exam); });
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