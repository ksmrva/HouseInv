using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Houses
{
    [Table("House")]
    public record House
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("address1")]
        public required string Address1 { get; set; }

        [Column("address2")]
        public string? Address2 { get; set; }

        [Column("city")]
        public required string City { get; set; }

        [Column("state")]
        public required string State { get; set; }

        [Column("zip")]
        public required string Zip { get; set; }

        [Column("ownerId")]
        public required long OwnerId { get; set; }

        [Column("createdDate")]
        public required DateTime CreatedDate { get; set; }

        [Column("modifiedDate")]
        public required DateTime ModifiedDate { get; set; }

        [Column("createdUser")]
        public required string CreatedUser { get; set; }

        [Column("modifiedUser")]
        public required string ModifiedUser { get; set; }
    }
}