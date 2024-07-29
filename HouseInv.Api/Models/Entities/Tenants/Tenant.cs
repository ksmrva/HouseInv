using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Api.Models.Entities.Tenants
{
    [Table("tenant")]
    public class Tenant
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("house_id")]
        public required long HouseId { get; init; }

        [Column("person_id")]
        public required long PersonId { get; init; }

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