using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("PRIVATE_MESSAGE")]
    public class PrivateMessage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("MESSAGE_ID")]
        public int MessageId { get; set; }


        public DateTime TimeSent { get; set; }


        public string MessageContent { get; set; }


        public int UserId { get; set; }


        public int ReplyMessageId { get; set; }

    }
}
