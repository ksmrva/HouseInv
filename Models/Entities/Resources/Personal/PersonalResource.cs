using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Resources.Personal
{
    [Table("PersonalResource")]
    public record PersonalResource
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("tenantId")]
        public required long TenantId { get; init; }

        [Column("resourceId")]
        public required long ResourceId { get; init; }

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