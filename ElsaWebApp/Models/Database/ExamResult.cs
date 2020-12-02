using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("EXAM_RESULT")]
    public class ExamResult
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("EXAM_RESULT_ID")]
        public int ExamResultId { get; set; }

        [Column("EXAM_SCORE")]
        public string ExamScore { get; set; }

        [Column("TIME_SCORED")]
        public DateTime TimeScored { get; set; }

        [Column("PROFFESORS_COMMENTARY")]
        public string ProfessorsCommentary { get; set; }

        [Column("STUDENT_ID")]
        public int StudentId { get; set; }

        [Column("EXAM_ID")]
        public int ExamId { get; set; }

    }
}
