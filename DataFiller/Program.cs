using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataFiller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            UserAdd add = new UserAdd();
            await add.AddUsers(50);
        }
    }
}