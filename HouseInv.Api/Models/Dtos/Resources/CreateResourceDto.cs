namespace HouseInv.Api.Models.Dtos.Resources
{
    public record CreateResourceDto
    {
        public required long HouseId { get; init; }
        public required string Name { get; init; }
        public required string UserId { get; init; }
    }
}