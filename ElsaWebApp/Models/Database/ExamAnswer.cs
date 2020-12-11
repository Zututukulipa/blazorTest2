using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("EXAM_QUESTION_ANSWER")]
    public class ExamAnswer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("ANSWER_ID")]
        public int AnswerId { get; set; }

        [Column("ANSWER_CONTENT")]
        public string AnswerContent { get; set; }

        [Column("QUESTION_ID")]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public ExamQuestion Question { get; set; }

        [Column("CORRECT")]
        public int Correct { get; set; }

        public ICollection<UserAnswers> Users { get; set; }
    }
}