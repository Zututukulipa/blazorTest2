using System;
using ElsaWebApp.Models.Database;

namespace ElsaWebApp.Models.DTO
{
    public class ApplicationUser
    {
        public int UserId { get; set; }
        public string UserUid { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public int Role { get; set; }
        public Address Address { get; set; }

        public ApplicationUser(User user)
        {
            UserId = user.Id;
            UserUid = user.UserUid;
            Firstname = user.Firstname;
            Surname = user.Surname;
            BirthDate = user.BirthDate;
            Phone = user.Phone;
            Email = user.Email;
            ProfilePicture = user.ProfilePicture;
            Role = user.RoleRoleId;
        }
    }
}