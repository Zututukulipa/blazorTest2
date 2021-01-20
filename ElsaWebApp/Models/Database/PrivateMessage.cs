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

        [Column("TIME_SENT")]
        public DateTime TimeSent { get; set; }

        [Column("MESSAGE_CONTENT")]
        public string MessageContent { get; set; }

    }
}
