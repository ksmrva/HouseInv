using HouseInv.Api.Models.Entities.Persons;

namespace HouseInv.Api.Models.Dtos.Houses
{
    public record HouseDto
    {
        public required long Id { get; init; }
        public required string Name { get; init; }
        public required string Address1 { get; init; }
        public string? Address2 { get; init; }
        public required string City { get; init; }
        public required string State { get; init; }
        public required string Zip { get; init; }
        public required long OwnerId { get; init; }
        public Person Owner { get; set; } = null;
        public required DateTime CreatedDate { get; init; }
        public required DateTime ModifiedDate { get; init; }
        public required string CreatedUser { get; init; }
        public required string ModifiedUser { get; init; }
    }
}