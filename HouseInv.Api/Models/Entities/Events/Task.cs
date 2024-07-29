using System.ComponentModel.DataAnnotations.Schema;
using HouseInv.Api.Models.Entities.Resources;

namespace HouseInv.Api.Models.Entities.Events
{
    [Table("task")]
    public class Task
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("name")]
        public required string Name { get; init; }

        [Column("resource")]
        public required Resource Resource { get; init; }

        [Column("assigned_action")]
        public required AssignedAction[] AssignedActions { get; init; }

        [Column("active_date")]
        public DateTime ActiveDate { get; set; }

        [Column("completed_date")]
        public DateTime CompletedDate { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; init; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}