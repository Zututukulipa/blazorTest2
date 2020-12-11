using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("USER_GROUPS")]
    public class UserGroups
    {
        [Column("GROUP_ID")]
        public int GroupId { get; set; }
        
        [ForeignKey("GroupId")]
        public UserGroup Group { get; set; }
        
        [Column("USER_ID")]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public DbUser User { get; set; }
    }
}
