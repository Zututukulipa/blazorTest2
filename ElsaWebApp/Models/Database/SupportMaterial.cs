using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("SUPPORT_MATERIAL")]
    public class SupportMaterial
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("MATERIAL_ID")]
        public int MaterialId { get; set; }

        [Column("MATERIAL_CREATED")]
        public DateTime MaterialCreated { get; set; }

        [Column("MATERIAL_LAST_CHANGE")]
        public DateTime? MaterialLastChange { get; set; }

        [Column("MATERIAL_NAME")]
        public string MaterialName { get; set; }

        [Column("PAGE_COUNT")]
        public int PageCount { get; set; }

        [Column("FILE_PATH")]
        public string FilePath { get; set; }

        [Column("SUBJECT_ID")]
        public int SubjectId { get; set; }

    }
}
