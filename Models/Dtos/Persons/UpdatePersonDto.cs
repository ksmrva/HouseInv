namespace HouseInv.Models.Dtos.Persons
{
    public record UpdatePersonDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserId { get; set; }
    }
}