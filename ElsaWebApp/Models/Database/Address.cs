using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElsaWebApp.Models.Database
{
    [Table("ADDRESS")]
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity),Column("ADDRESS_ID")]
        public int AddressId { get; set; }

        [Column("STREET")]
        public string Street { get; set; }

        [Column("HOUSE_NR")]
        public string HouseNr { get; set; }

        [Column("CITY")]
        public string City { get; set; }

        [Column("ZIP")]
        public string Zip { get; set; }

    }
}
