namespace HouseInv.Models.Dtos.Persons
{
    public record CreatePersonDto
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string UserId { get; init; }
    }
}