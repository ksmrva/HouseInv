using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Resources.Personal.Appliance
{
    [Table("Appliance")]
    public record Appliance
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("personalResourceId")]
        public required long PersonalResourceId { get; init; }

        [Column("brand")]
        public required string Brand { get; init; }

        [Column("price")]
        public required decimal Price { get; init; }

        [Column("purchaseDate")]
        public required DateTime PurchaseDate { get; init; }

        [Column("installationDate")]
        public required DateTime InstallationDate { get; init; }

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