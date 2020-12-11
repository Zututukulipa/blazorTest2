using System;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Controllers.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataFiller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
               var add = new SubjectAdd(new HttpClient());
               await add.PairSubjectsWithStudents();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    Console.WriteLine("\n");
                    Console.WriteLine(e);
                }
            }
        }
    }
}