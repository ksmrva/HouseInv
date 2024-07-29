using System.ComponentModel.DataAnnotations.Schema;

namespace HouseInv.Api.Models.Entities.Persons
{
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public long Id { get; init; }

        [Column("first_name")]
        public required string FirstName { get; init; }

        [Column("last_name")]
        public required string LastName { get; init; }

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