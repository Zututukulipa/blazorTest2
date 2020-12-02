using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("DBUSER")]
    public class DbUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("USER_ID")]
        public int UserId { get; set; }

        [Column("USER_UID")]
        public string UserUid { get; set; }

        [Column("FIRSTNAME")]
        public string Firstname { get; set; }

        [Column("SURNAME")]
        public string Surname { get; set; }

        [Column("BIRTH_DATE")]
        public DateTime BirthDate { get; set; }

        [Column("PHONE")]
        public string Phone { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("USER_PASSWORD")]
        public string UserPassword { get; set; }

        [Column("STUDY_YEAR")]
        public string StudyYear { get; set; }

        [Column("PROFILE_PICTURE")]
        public string ProfilePicture { get; set; }

        [Column("TIME_CREATED")]
        public DateTime TimeCreated { get; set; }

        [Column("ROLE_ROLE_ID")]
        public int RoleId { get; set; }

        [Column("ADDRESS_ADDRESS_ID")]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        [Column("SALT")]
        public string Salt { get; set; }

        public IEnumerable<UserGroups> UserGroups { get; set; }
        public IEnumerable<UserSubjects> UserSubjects { get; set; }

        public override string ToString()
        {
            return UserId < 1 ? "NULL" : $"{Firstname} {Surname}\n{UserUid}\n{Email}\n{Phone}\nAddress:\n{Address}";
        }
    }
}