using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Events
{
    [Table("Action")]
    public class Action
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("name")]
        public required string Name { get; init; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; init; }

        [Column("modifiedDate")]
        public DateTime ModifiedDate { get; set; }
    }
}