using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("USER_ANSWERS")]
    public class UserAnswers
    {
        [Column("USER_ID")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public DbUser User { get; set; }
        
        [Column("ANSWER_ID")]
        public int AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public ExamAnswer Answer { get; set; }
        
    }
}