using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("CLASSROOM_SUBJECTS")]
    public class ClassroomSubjects
    {
        [Column("SUBJECT_ID")]
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        [Column("CLASSROOM_ID")]
        public int ClassroomId { get; set; }
        [ForeignKey("ClassroomId")] 
        public Classroom Classroom { get; set; }
    }
}
