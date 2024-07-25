using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Resources.Personal.Appliance
{
    [Table("appliance")]
    public class Appliance
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("personal_resource_id")]
        public required long PersonalResourceId { get; init; }

        [Column("brand")]
        public required string Brand { get; init; }

        [Column("price")]
        public required decimal Price { get; init; }

        [Column("purchase_date")]
        public required DateTime PurchaseDate { get; init; }

        [Column("installation_date")]
        public required DateTime InstallationDate { get; init; }

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