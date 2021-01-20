using System;
using ElsaWebApp.Models.Database;

namespace ElsaWebApp.Utils
{
    public class DataGenerationUtil
    {
        public static string GenerateUserEmail(DbUser user, string domain = "schoold.com", bool obfuscate = false)
        {
            if (!obfuscate)
            {
                return $"{user.Firstname.ToLower()}.{user.Surname.ToLower()}@{domain}";
            }
            else
            {
                var rnd = new Random();
                return $"{user.Firstname.ToLower()}." +
                       $"{user.Surname.ToLower()}{StringUtils.ScrambleLetters(user.BirthDate.ToString("yy-MM-dd").Replace('-','\0'))}@{domain}";
            }
        }
    }
}