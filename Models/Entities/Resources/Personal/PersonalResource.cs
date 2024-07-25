using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Resources.Personal
{
    [Table("personal_resource")]
    public class PersonalResource
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("tenant_id")]
        public required long TenantId { get; init; }

        [Column("resource_id")]
        public required long ResourceId { get; init; }

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