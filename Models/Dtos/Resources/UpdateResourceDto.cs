namespace HouseInv.Models.Dtos.Resources
{
    public record UpdateResourceDto
    {
        public required long HouseId { get; init; }
        public required string Name { get; set; }
        public required string UserId { get; set; }
    }
}