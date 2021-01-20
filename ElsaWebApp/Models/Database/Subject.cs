using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("SUBJECT")]
    public class Subject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("SUBJECT_ID")]
        public int SubjectId { get; set; }

        [Column("SUBJECT_NAME")]
        public string SubjectName { get; set; }

        [Column("SEMESTER")]
        public string Semester { get; set; }

        [Column("MAX_CAPACITY")]
        public int MaxCapacity { get; set; }

        [Column("CURRENT_CAPACITY")]
        public int CurrentCapacity { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("GARANT_ID")]
        public int GarantId { get; set; }

        [ForeignKey("GarantId")]
        public DbUser Garant { get; set; }

        public virtual ICollection<UserSubjects> UserSubjects { get; set; }
        public virtual ICollection<ClassroomSubjects> Classrooms { get; set; }

        public override string ToString()
        {
            return $"{SubjectName}\n{Description}\nMax:{MaxCapacity}, Current:{CurrentCapacity}\n{Semester}";
        }
    }
}
