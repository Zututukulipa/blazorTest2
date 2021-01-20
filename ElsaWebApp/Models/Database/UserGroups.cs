using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("USER_GROUPS")]
    public class UserGroups
    {
        [Column("GROUP_ID")]
        public int GroupId { get; set; }
        
        public virtual UserGroup Group { get; set; }
        
        [Column("USER_ID")]
        public int UserId { get; set; }
        
        public virtual DbUser User { get; set; }
    }
}
