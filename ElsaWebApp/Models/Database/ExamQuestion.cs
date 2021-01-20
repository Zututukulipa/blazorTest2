using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("EXAM_QUESTION")]
    public class ExamQuestion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("QUESTION_ID")]
        public int QuestionId { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }
        
        [Column("POINTS")]
        public int Points { get; set; }
        
        [Column("EXAM_ID")]
        public int ExamId { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
        
        [Column("TYPE_ID")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public AnswerType ExamType { get; set; }

        public virtual ICollection<ExamAnswer> Answers { get; set; }
    }
}