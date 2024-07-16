using System.ComponentModel.DataAnnotations.Schema;
using HouseInv.Models.Entities.Resources;

namespace HouseInv.Models.Entities.Events
{
    [Table("Task")]
    public class Task
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("name")]
        public required string Name { get; init; }

        [Column("resource")]
        public required Resource Resource { get; init; }

        [Column("assignedAction")]
        public required AssignedAction[] AssignedActions { get; init; }

        [Column("activeDate")]
        public DateTime ActiveDate { get; set; }

        [Column("completedDate")]
        public DateTime CompletedDate { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; init; }

        [Column("modifiedDate")]
        public DateTime ModifiedDate { get; set; }
    }
}