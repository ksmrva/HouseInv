using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Api.Models.Entities.Events
{
    [Table("action")]
    public class Action
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("name")]
        public required string Name { get; init; }

        [Column("created_date")]
        public DateTime CreatedDate { get; init; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}