using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("ROLE")]
    public class DbRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("ROLE_ID")]
        public int RoleId { get; set; }

        [Column("ROLE_NAME")]        
        public string RoleName { get; set; }

    }
}
