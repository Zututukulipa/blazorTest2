using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("USER_GROUPS")]
    public class UserGroups
    {

        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public UserGroup Group { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public DbUser User { get; set; }

    }
}
