using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("MESSAGE_RECIPIENT")]
    public class MessageRecipient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("MESSAGE_TRANSACTION_ID")]
        public int MessageTransactionId { get; set; }
        
        [Column("SENDER_ID")] public int SenderId { get; set; }
        [ForeignKey("SenderId")] public DbUser Sender { get; set; }

        [Column("MESSAGE_ID")] public int MessageId { get; set; }
        [ForeignKey("MessageId")] public PrivateMessage Message { get; set; }

        [Column("GROUP_ID")] public int? GroupId { get; set; }
        [ForeignKey("GroupId")] public UserGroup Group { get; set; }

        [Column("RECIPIENT_ID")] public int? RecipientId { get; set; }
        [ForeignKey("RecipientId")] public DbUser Recipient { get; set; }
    }
}