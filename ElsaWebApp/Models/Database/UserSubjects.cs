using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("USER_SUBJECTS")]
    public class UserSubjects
    {

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public DbUser Student { get; set; }
       
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

    }
}
