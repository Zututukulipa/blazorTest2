using System;
using Microsoft.AspNetCore.Identity;

namespace ElsaWebApp.Models
{
    public class User 
    {
        public int Id { get; set; }

        public string UserUid { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string UserPassword { get; set; }

        public int StudyYear { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime TimeCreated { get; set; }

        public int RoleRoleId { get; set; }

        public int AddressAddressId { get; set; }

        public string Salt { get; set; }
    }
}