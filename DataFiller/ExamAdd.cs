using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Services.DataAccess;

namespace DataFiller
{
    public class ExamAdd
    {
        private readonly HttpClient _client;
        private readonly ExamService _service;
        private readonly UserService _users;
        private readonly SubjectService _classes;
        private Random _random = new Random();

        public ExamAdd(HttpClient client)
        {
            _client = new HttpClient();
            _service = new ExamService(_client);
            _users = new UserService(_client);
            _classes = new SubjectService(_client);
        }

        public async Task<bool> AddExams(int amount)
        {
            var classrooms = await GenerateExams(amount);
            return await _service.InsertExams(classrooms);
        }

        private async Task<List<Exam>> GenerateExams(int amount)
        {
            List<Exam> classrooms = new List<Exam>();
            var users = await _users.GetUsers();
            var professors = users.FindAll(p => p.RoleId == 2).ToList();
            var subjects = await _classes.GetSubjects();
            for (int i = 0; i < amount; i++)
            {
                var subject = subjects[_random.Next(subjects.Count)];
                var room = new Exam()
                {
                    ExamName = $"E{i}", CreatorId = subject.GarantId,
                    ExamStart = GetRandomDate(DateTime.Now, DateTime.Now.AddDays(31)),
                    SubjectId = subject.SubjectId,
                    TypeId = _random.Next(1, 2)
                };

                classrooms.Add(room);
            }

            return classrooms;
        }

        private DateTime GetRandomDate(DateTime from, DateTime to)
        {
            var range = to - from;

            var randTimeSpan = new TimeSpan((long) ((_random).NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }


        public async Task<bool> GenerateAnswers()
        {
            var users = await _users.GetUsers();
            var students = users.FindAll(s => s.RoleId == 3).ToList();
            var questions = await _service.GetQuestions();
            var allAnswers = await _service.GetAnswers();

            return await _service.InsertAnswers((from student in students
                from question in questions
                let questionAnswers = allAnswers.FindAll(q => q.QuestionId == question.QuestionId).ToList()
                where questionAnswers.Count > 0
                let pickedAnswer = questionAnswers[_random.Next(questionAnswers.Count)]
                select new UserAnswers {AnswerId = pickedAnswer.AnswerId, UserId = student.UserId}).ToList());
        }

        public async Task CreateGradings()
        {
            var users = await _users.GetUsers();
            var students = users.FindAll(s => s.RoleId == 3);
            var allAnswers = await _service.GetQuestionAnswers();
            var exams = allAnswers.Select(e => e.ExamId).Distinct().ToArray();
            var gradings = new List<ExamResult>();
            foreach (var student in students)
            {
                foreach (var eid in exams)
                {
                    var correct = allAnswers.FindAll(ua => ua.Correct == 1 && ua.UserId == student.UserId);
                    var points = correct.Select(ua => ua.Points).Sum();
                    
                    ExamResult result = new ExamResult(){ExamScore = points.ToString(), StudentId = student.UserId, ProfessorsCommentary = "You are dumb", TimeScored = DateTime.Now, ExamId = eid};
                    gradings.Add(result);
                }
            }

            await _service.InsertResults(gradings);

        }


        private async Task<ExamQuestion> GenerateExamQuestion(Exam exam)
        {
            var question = new ExamQuestion()
            {
                TypeId = _random.Next(1, 2),
                Description = "Test description, try to get random answer",
                Points = 5,
                ExamId = exam.ExamId
            };
            return await _service.InsertQuestion(question);
        }

        private static void GenerateAnswers(int i, int correct, ExamQuestion question, List<ExamAnswer> answers)
        {
            if (i == correct)
            {
                var answer = new ExamAnswer()
                {
                    QuestionId = question.QuestionId,
                    AnswerContent = $"ANSWER{i}",
                    Correct = 1
                };
                answers.Add(answer);
            }
            else
            {
                var answer = new ExamAnswer()
                {
                    QuestionId = question.QuestionId,
                    AnswerContent = $"ANSWER{i}",
                    Correct = 0
                };
                answers.Add(answer);
            }
        }
    }
}