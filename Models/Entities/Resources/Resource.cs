using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Resources
{
    [Table("Resource")]
    public record Resource
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("houseId")]
        public required long HouseId { get; init; }

        [Column("name")]
        public required string Name { get; init; }

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