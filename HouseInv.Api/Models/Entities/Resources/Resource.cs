using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Api.Models.Entities.Resources
{
    [Table("resource")]
    public class Resource
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("house_id")]
        public required long HouseId { get; init; }

        [Column("name")]
        public required string Name { get; init; }

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