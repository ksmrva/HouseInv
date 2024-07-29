using System.ComponentModel.DataAnnotations.Schema;
using HouseInv.Api.Models.Entities.Resources.Personal.Appliance;

namespace HouseInv.Api.Models.Entities
{
    [Table("filter")]
    public class Filter : AppliancePart
    {
        [Column("purchase_url")]
        public string? PurchaseUrl { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("time_until_replacement_in_weeks")]
        public int TimeUntilReplacementInWeeks { get; set; }

        [Column("installation_date")]
        public DateTime InstallationDate { get; set; }

        [Column("last_purchase_date")]
        public DateTime LastPurchaseDate { get; init; }
    }
}