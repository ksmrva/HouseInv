namespace HouseInv.Models.Dtos.Persons
{
    public record PersonDto
    {
        public required long Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateTime CreatedDate { get; init; }
        public required DateTime ModifiedDate { get; init; }
        public required string CreatedUser { get; init; }
        public required string ModifiedUser { get; init; }
    }
}