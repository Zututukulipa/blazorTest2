using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("COMMENT")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("COMMENT_ID")]
        public int CommentId { get; set; }

        [Column("DATE_CREATED")]
        public DateTime DateCreated { get; set; }

        [Column("CONTENT")]
        public string Content { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("EXAM_ID")]
        public int ExamId { get; set; }

        [Column("MATERIAL_ID")]
        public int MaterialId { get; set; }

    }
}
