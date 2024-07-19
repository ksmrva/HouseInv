using System.ComponentModel.DataAnnotations.Schema;
using HouseInv.Models.Entities.Resources.Personal.Appliance;

namespace HouseInv.Models.Entities
{
    [Table("Filter")]
    public class Filter : AppliancePart
    {
        [Column("purchaseUrl")]
        public string? PurchaseUrl { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("timeUntilReplacementInWeeks")]
        public int TimeUntilReplacementInWeeks { get; set; }

        [Column("installationDate")]
        public DateTime InstallationDate { get; set; }

        [Column("lastPurchaseDate")]
        public DateTime LastPurchaseDate { get; init; }
    }
}