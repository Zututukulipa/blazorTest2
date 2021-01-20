using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Models.Views;
using Newtonsoft.Json;

namespace ElsaWebApp.Services.DataAccess
{
    public class ExamService
    {
        private HttpClient _http;
        public ExamService(HttpClient client)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _http = client;
            _http.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<List<Exam>> GetExams()
        {
            var exams = await _http.GetFromJsonAsync<List<Exam>>("api/exam/GetAll");
            return exams;
        }

        public async Task<Exam> GetExamById(int id)
        {
            var exam = await _http.GetFromJsonAsync<Exam>($"api/exam/{id}");
            return exam;
        }

        public async Task<bool> InsertExam(Exam exam)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/exam", exam);
            return responseMessage.IsSuccessStatusCode;
        }
        
        public async Task<bool> InsertExams(List<Exam> exams)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/exam/addRange", exams);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateExam(Exam exam)
        {
            var responseMessage = await _http.PutAsJsonAsync("api/exam", exam);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<ExamQuestion> InsertQuestion(ExamQuestion question)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/exam/question", question);
            if (responseMessage.Content != null)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExamQuestion>(jsonString);
            }

            return null;
        }

        public async Task<bool> InsertAnswers(List<UserAnswers> answers)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/exam/question/answer/submit", answers);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<ExamQuestion>> GetQuestions()
        {
            var examQuestions = await _http.GetFromJsonAsync<List<ExamQuestion>>("exam/question/GetAll");
            return examQuestions;
        }
        
        public async Task<List<ExamQuestion>> GetQuestions(int id)
        {
            var examQuestions = await _http.GetFromJsonAsync<List<ExamQuestion>>("api/exam/question/10");
            return examQuestions;
        }

        public async Task<List<ExamAnswer>> GetAnswers()
        {
            var examAnswers = await _http.GetFromJsonAsync<List<ExamAnswer>>("api/exam/question/answer/getAll");
            return examAnswers;
        }
        
        public async Task<bool> InsertQuestionAnswers(List<UserAnswers> answers)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/exam/question/answer/submit", answers);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<UserAnswers>> GetUserAnswers()
        {
            return await _http.GetFromJsonAsync<List<UserAnswers>>("api/exam/question/answers/getAll");
        }

        public async Task<List<UserExamAnswersView>> GetQuestionAnswers()
        {
            return await _http.GetFromJsonAsync<List<UserExamAnswersView>>("api/exam/question/answered/getAll");
        }

        public async Task<bool> InsertResults(List<ExamResult> gradings)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/exam/results/AddRange", gradings);
            return responseMessage.IsSuccessStatusCode;
        }
    }
}