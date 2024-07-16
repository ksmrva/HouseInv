using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Protections
{
    [Table("Warranty")]
    public record Warranty
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("name")]
        public required string Name { get; init; }

        [Column("lengthInWeeks")]
        public required int LengthInWeeks { get; init; }

        [Column("purchaseDate")]
        public required DateTime PurchaseDate { get; init; }

        [Column("createdDate")]
        public required DateTime CreatedDate { get; init; }

        [Column("modifiedDate")]
        public required DateTime ModifiedDate { get; init; }

        [Column("createdUser")]
        public required string CreatedUser { get; init; }

        [Column("modifiedUser")]
        public required string ModifiedUser { get; init; }
    }
}