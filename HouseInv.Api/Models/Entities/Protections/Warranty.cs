using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Api.Models.Entities.Protections
{
    [Table("warranty")]
    public class Warranty
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("name")]
        public required string Name { get; init; }

        [Column("length_in_weeks")]
        public required int LengthInWeeks { get; init; }

        [Column("purchase_date")]
        public required DateTime PurchaseDate { get; init; }

        [Column("created_date")]
        public required DateTime CreatedDate { get; init; }

        [Column("modified_date")]
        public required DateTime ModifiedDate { get; init; }

        [Column("created_user")]
        public required string CreatedUser { get; init; }

        [Column("modified_user")]
        public required string ModifiedUser { get; init; }
    }
}