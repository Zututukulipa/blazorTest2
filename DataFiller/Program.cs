using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ElsaWebApp.Controllers.DataAccess;
using ElsaWebApp.Models.Database;
using HumanLab;
using Microsoft.EntityFrameworkCore;

namespace DataFiller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var add = new UserAdd();
                await add.AddProfessors(20);
                await add.AddUsers(100);
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