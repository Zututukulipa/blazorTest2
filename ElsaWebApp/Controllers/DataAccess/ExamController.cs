using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Models.Views;
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
        
        [HttpGet("getAll")]
        public async Task<ActionResult<List<Exam>>> GetExams()
        {
            var exams = await Context.Exams.ToListAsync();
            if(exams.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(exams);
        }
        
        [HttpGet("question/getAll")]
        public async Task<ActionResult<List<ExamQuestion>>> GetQuestions()
        {
            var questions = await Context.ExamQuestions.ToListAsync();
            if(questions.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(questions);
        }
        
        [HttpPost("question/answer/submit")]
        public async Task<ActionResult> SubmitAnswers(List<UserAnswers> answers)
        {
            try
            {
                await Context.UserAnswers.AddRangeAsync(answers);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpGet("question/answers/getAll")]
        public async Task<ActionResult<List<UserAnswers>>> GetUserAnswers()
        {
            var answers = await Context.UserAnswers.ToListAsync();
            if(answers.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(answers);
        }
        
        [HttpGet("question/answer/getAll")]
        public async Task<ActionResult<List<ExamAnswer>>> GetAnswers()
        {
            var questions = await Context.ExamAnswers.ToListAsync();
            if(questions.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(questions);
        }
        
        [HttpGet("question/{id}")]
        public async Task<ActionResult<List<ExamQuestion>>> GetQuestions(int id)
        {
            List<ExamQuestion> questions = await Context.ExamQuestions.Where(e => e.ExamId == id).ToListAsync();
            if(questions.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(questions);
        }
        
        [HttpGet("question/answered/getAll")]
        public async Task<ActionResult<List<UserExamAnswersView>>> GetAnswered()
        {
            var questions = await Context.UserExamAnswersView.ToListAsync();
            if(questions.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(questions);
        }
        
        [HttpGet("question/answer/{id}")]
        public async Task<ActionResult<List<ExamAnswer>>> GetAsnwers(int id)
        {
            List<ExamAnswer> questions = await Context.ExamAnswers.Where(a => a.QuestionId == id).ToListAsync();
            if(questions.Count == 0)
                return new NotFoundResult();
            return new OkObjectResult(questions);
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
                return new OkObjectResult(exam);
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("addRange")]
        public async Task<ActionResult> InsertExam(List<Exam> exams)
        {
            try
            {
                await Context.Exams.AddRangeAsync(exams);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("answerAddRange")]
        public async Task<ActionResult> InsertAnswer(List<ExamAnswer> answers)
        {
            try
            {
                await Context.ExamAnswers.AddRangeAsync(answers);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("questionAddRange")]
        public async Task<ActionResult> InsertQuestion(List<ExamQuestion> questions)
        {
            try
            {
                await Context.ExamQuestions.AddRangeAsync(questions);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("results/AddRange")]
        public async Task<ActionResult> InsertExamResults(List<ExamResult> results)
        {
            try
            {
                await Context.ExamResults.AddRangeAsync(results);
                await Context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception _)
            {
                return new BadRequestResult();
            }
        }
        
        [HttpPost("question")]
        public async Task<ActionResult> InsertQuestion(ExamQuestion question)
        {
            try
            {
                await Context.ExamQuestions.AddAsync(question);
                await Context.SaveChangesAsync();
                return new OkObjectResult(question);
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