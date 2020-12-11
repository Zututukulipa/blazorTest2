using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("USER_GROUP")]
    public class UserGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("GROUP_ID")]
        public int GroupId { get; set; }

        [Column("GROUP_NAME")]
        public string GroupName { get; set; }

        [Column("LEADER_ID")]
        public int LeaderId { get; set; }

        public virtual ICollection<UserGroups> UserGroups { get; set; }
        
    }
}
