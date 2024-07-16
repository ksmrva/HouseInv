using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Models.Entities.Persons
{
    [Table("Person")]
    public record Person
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("firstName")]
        public required string FirstName { get; init; }

        [Column("lastName")]
        public required string LastName { get; init; }

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