using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oracle.ManagedDataAccess.Client;

namespace ElsaWebApp.Models.Database
{
    [Table("DBUSER")]
    public class DbUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("USER_ID")]
        public int UserId { get; set; }

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
        public int StudyYear { get; set; }

        [Column("PROFILE_PICTURE", TypeName = "blob")]
        public Byte[] ProfilePicture { get; set; }

        [Column("TIME_CREATED")]
        public DateTime TimeCreated { get; set; }

        [Column("ROLE_ID")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public DbRole Role { get; set; }

        [Column("ADDRESS_ID")]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        [Column("SALT")]
        public string Salt { get; set; }

        public virtual ICollection<UserGroups> UserGroups { get; set; }
        public virtual ICollection<UserSubjects> UserSubjects { get; set; }
        public virtual ICollection<UserAnswers> Answers { get; set; }

        public override string ToString()
        {
            return UserId < 1 ? "NULL" : $"{Firstname} {Surname}\n{Email}\n{Phone}\nAddress:\n{Address}";
        }
    }
}