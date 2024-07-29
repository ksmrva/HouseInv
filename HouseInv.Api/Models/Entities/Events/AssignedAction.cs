using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Api.Models.Entities.Events
{
    [Table("assigned_action")]
    public class AssignedAction
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("action")]
        public required Action Action { get; init; }

        [Column("completed")]
        public required bool Completed { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; init; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}