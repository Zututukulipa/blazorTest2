using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ElsaWebApp.Models.Database;

namespace ElsaWebApp.Services.DataAccess
{
    public class SubjectService
    {
        private HttpClient _http;
        public SubjectService(HttpClient client)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _http = client;
            _http.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<List<Subject>> GetSubjects()
        {
            var subjects = await _http.GetFromJsonAsync<List<Subject>>("api/subject/GetAll");
            return subjects;
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            var subject = await _http.GetFromJsonAsync<Subject>($"api/subject/{id}");
            return subject;
        }

        public async Task<bool> InsertSubject(Subject subject)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/subject", subject);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> InsertSubjects(List<Subject> subjects)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/subject/addrange", subjects);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateSubject(Subject subject)
        {
            var responseMessage = await _http.PutAsJsonAsync("api/subject", subject);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> PairSubjectWithClass(Subject subject, Classroom classroom)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/pairSubject",
                new ClassroomSubjects(){Classroom =  classroom, Subject = subject});
            return responseMessage.IsSuccessStatusCode;
        }
        
        public async Task<bool> PairSubjectWithClass(List<(Classroom classroom, Subject subject)> pairs)
        {
            List<ClassroomSubjects> list = new List<ClassroomSubjects>();
            foreach (var pair in pairs)
            {
                list.Add(new ClassroomSubjects()
                {
                    ClassroomId = pair.classroom.ClassroomId,
                    SubjectId = pair.subject.SubjectId
                });
            }
            var responseMessage = await _http.PostAsJsonAsync("api/subject/pairSubjectRange", list);
            return responseMessage.IsSuccessStatusCode;
        }


        public async Task<bool> PairSubjectWithStudents(List<UserSubjects> pairs)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/subject/pairSubjectStudentRange", pairs);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<Subject>> GetSubjectsRead()
        {
            var subjects = await _http.GetFromJsonAsync<List<Subject>>("api/subject/GetAllRead");
            return subjects;
        }
    }
}