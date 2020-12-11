using ElsaWebApp.Models.Database;
using HumanLab;
using Address = ElsaWebApp.Models.Database.Address;

namespace DataFiller
{
    public class Mapper
    {
        public static DbUser GetUser(Person person)
        {
            var address = new Address(){City = person.Address.City, 
                                        Street = person.Address.Street,
                                        HouseNr = person.Address.HouseNr,
                                        Zip = person.Address.Zip
                                        };
            return new DbUser(){Address = address, Email = person.Email, 
                                Firstname = person.FirstName, Surname = person.LastName,
                                Phone = person.PhoneNumber.Replace(" ", ""), BirthDate = person.BirthDate,
                                RoleId = 3
            };
        }

        public static DbUser GetProfessor(Person input)
        {
            var user = GetUser(input);
            user.RoleId = 2;
            return user;
        }
    }
}