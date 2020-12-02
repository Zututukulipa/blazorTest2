using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("EXAM_TYPE")]
    public class ExamType
    {
        [Key, Column("TYPE_ID")]
        public int TypeId { get; set; }

        [Column("TYPE_NAME")]
        public string TypeName { get; set; }

    }
}
