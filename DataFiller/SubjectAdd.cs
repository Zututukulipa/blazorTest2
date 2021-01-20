using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using ElsaWebApp.Models.Database;
using ElsaWebApp.Services.DataAccess;

namespace DataFiller
{
    public class SubjectAdd
    {
        private readonly SubjectService _service;
        private readonly UserService _userService;
        private readonly ClassroomService _classroomService;
        private HttpClient _client;
        private Random _random = new Random();

        public SubjectAdd(HttpClient client)
        {
            _client = client;
            _service = new SubjectService(client);
            _userService = new UserService(client);
            _classroomService = new ClassroomService(client);
        }

        public async Task<bool> PairSubjectsWithClassrooms()
        {
            var classrooms = await _classroomService.GetClassrooms();
            if(classrooms == null || classrooms.Count < 1)
                throw new Exception("Insert classrooms into database prior");
            var subjects = await _service.GetSubjects();
            if(subjects == null || subjects.Count < 1)
                throw new Exception("Insert subjects into database prior");

            var pairs = GeneratePairs(classrooms, subjects);

            var success = await _service.PairSubjectWithClass(pairs);
            return success;
        }
        
        public async Task<bool> PairSubjectsWithStudents()
        {
            var users = await _userService.GetUsers();
            var students = users.FindAll(u => u.RoleId == 3);
            if(students == null || students.Count < 1)
                throw new Exception("Insert classrooms into database prior");
            List<Subject> subjects = await _service.GetSubjects();
            if(subjects == null || subjects.Count < 1)
                throw new Exception("Insert subjects into database prior");

            var pairs = new List<UserSubjects>();

            for(int j = 0; j < 500; ++j)
            {
                var subject = subjects[j];
                for (int i = 0; i < _random.Next(5,20); i++)
                {
                    UserSubjects pair = new UserSubjects()
                    {
                        StudentId = students[_random.Next(students.Count)].UserId,
                        SubjectId = subject.SubjectId
                    };
                    
                    if(!pairs.Contains(pair))
                        pairs.Add(pair);
                    else
                        Console.WriteLine("click");
                }
            }
            bool success = await _service.PairSubjectWithStudents(pairs);
            return success;
        }

        private List<(Classroom Classroom, Subject ClassroomSubjects)> GeneratePairs(List<Classroom> classrooms, List<Subject> subjects)
        {
            var pairs = new List<(Classroom, Subject)>();
            foreach (var classroom in classrooms)
            {
                for (int i = 0; i < 5; i++)
                {
                    var subject = subjects[_random.Next(subjects.Count)];
                    var pair = (classroom, subject);

                    if (!pairs.Contains(pair))
                        pairs.Add(pair);
                }
            }

            return pairs;
        }

        public async Task<bool> AddSubjects()
        {
            var classrooms = await GenerateSubjects();
            return await _service.InsertSubjects(classrooms);
        }

        private async Task<List<Subject>> GenerateSubjects()
        {
            List<DbUser> users = await _userService.GetUsers();
            var professors = users.FindAll(u => u.RoleId == 2);
            List<Subject> subjects = new List<Subject>();
            var serverResponse = await _client.GetAsync(
                "https://stag-ws.upce.cz/ws/services/rest2/predmety/getPredmetyByFakultaFullInfo?fakulta=FEI");
            serverResponse.EnsureSuccessStatusCode();
            var response = await serverResponse.Content.ReadAsStringAsync();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var name = ExtractText(node, "nazev");
                var desc = ExtractText(node, "anotace");
                if (desc.Length > 1000)
                    desc = desc.Substring(0, 1000)+"...";
                var sem = ExtractSemester(node, "vyukaZS");
                Subject subject = new Subject()
                {
                    SubjectName = name, Description = desc, GarantId = professors[_random.Next(professors.Count)].UserId,
                    Semester = sem, CurrentCapacity = 0, MaxCapacity = _random.Next(10,50)
                };
               
                subjects.Add(subject);
            }
            return await Task.FromResult(subjects);
        }
        
        private string ExtractSemester(XmlNode node, string hint)
        {
            try
            {
                var sem = node[hint].InnerText;
                return sem == "N" ? "S" : "W";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string ExtractText(XmlNode node, string name)
        {
            try
            {
                return node[name].InnerText;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}