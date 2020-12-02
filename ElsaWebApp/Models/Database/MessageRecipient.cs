using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("MESSAGE_RECIPIENT")]
    public class MessageRecipient
    {
        [Column("RECIPIENT_ID")]
        public int RecipientId { get; set; }

        [Column("MESSAGE_ID")]
        public int MessageId { get; set; }

        [Column("GROUP_ID")]
        public int GroupId { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

    }
}
