using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Events
{
    [Table("AssignedAction")]
    public class AssignedAction
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("action")]
        public required Action Action { get; init; }

        [Column("completed")]
        public required bool Completed { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; init; }

        [Column("modifiedDate")]
        public DateTime ModifiedDate { get; set; }
    }
}