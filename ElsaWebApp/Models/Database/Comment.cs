using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("COMMENTARY")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("COMMENT_ID")]
        public int CommentId { get; set; }

        [Column("DATE_CREATED")]
        public DateTime DateCreated { get; set; }

        [Column("CONTENT")]
        public string Content { get; set; }

        [Column("USER_ID")]
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public DbUser User { get; set; }
        
        [Column("REPLY_ID")]
        public int? ReplyId { get; set; }
        
        [ForeignKey("ReplyId")]
        public Comment Reply { get; set; }

        [Column("EXAM_ID")]
        public int? ExamId { get; set; }
        
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        [Column("MATERIAL_ID")]
        public int? MaterialId { get; set; }
        
        [ForeignKey("MaterialId")]
        public SupportMaterial Material { get; set; }

    }
}
