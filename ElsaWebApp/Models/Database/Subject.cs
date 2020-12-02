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

        [Column("GARANT_IT")]
        public int GarantId { get; set; }

        public IEnumerable<UserSubjects> Students { get; set; }
        public IEnumerable<ClassroomSubjects> Classrooms { get; set; }
    }
}
