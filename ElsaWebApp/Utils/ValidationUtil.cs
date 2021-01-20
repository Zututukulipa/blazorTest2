using System.Threading.Tasks;
using ElsaWebApp.Models.Database;

namespace ElsaWebApp.Utils
{
    public class ValidationUtil
    {
        public static Task<bool> IsUserInfoInvalid(DbUser user)
        {
            bool invalid = IsRegistrationInfoValid(user).Result;

            if (string.IsNullOrEmpty(user.Email))
                invalid = true;

            return Task.FromResult(invalid);
        }
        
        public static Task<bool> IsRegistrationInfoValid(DbUser user)
        {
            bool invalid = user.Address == null || string.IsNullOrEmpty(user.Firstname)
                           || string.IsNullOrEmpty(user.Surname) || string.IsNullOrEmpty(user.Phone)
                           || string.IsNullOrEmpty(user.UserPassword) || string.IsNullOrEmpty(user.Address.City)
                           || string.IsNullOrEmpty(user.Address.Street) || string.IsNullOrEmpty(user.Address.Zip)
                           || string.IsNullOrEmpty(user.Address.HouseNr);
            if (user.RoleId == 3 && user.StudyYear < 1)
                invalid = true;

            return Task.FromResult(invalid);
        }
    }
    
}