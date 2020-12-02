using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("CLASSROOM")]
    public class Classroom
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity),Column("CLASSROOM_ID")]
        public int ClassroomId { get; set; }

        [Column("CLASSROOM_NAME")]
        public string ClassroomName { get; set; }

        [Column("CLASSROOM_CAPACITY")]
        public int ClassroomCapacity { get; set; }

        [Column("IS_OPERATIONAL")]
        public bool IsOperational { get; set; }


        public IEnumerable<ClassroomSubjects> Subjects { get; set; }
    }
}
