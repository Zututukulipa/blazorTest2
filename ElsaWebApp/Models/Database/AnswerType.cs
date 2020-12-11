using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("ANSWER_TYPE")]
    public class AnswerType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("TYPE_ID")]
        public int TypeId { get; set; }

        [Column("TYPE_NAME")]
        public string TypeName { get; set; }
    }
}