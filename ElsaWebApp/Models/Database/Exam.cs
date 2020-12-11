using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("EXAM")]
    public class Exam
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("EXAM_ID")]
        public int ExamId { get; set; }

        [Column("EXAM_NAME")]
        public string ExamName { get; set; }

        [Column("EXAM_UID")]
        public string ExamUid { get; set; }

        [Column("EXAM_PASSWORD")]
        public string ExamPassword { get; set; }

        [Column("EXAM_START")]
        public DateTime ExamStart { get; set; }

        [Column("EXAM_END")]
        public DateTime? ExamEnd { get; set; }

        [Column("TYPE_ID")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public ExamType ExamType { get; set; }
        
        [Column("SUBJECT_ID")]
        public int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        [Column("CREATOR_ID")]
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public DbUser Creator { get; set; }

    }
}
