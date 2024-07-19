namespace HouseInv.Models.Dtos.Resources
{
    public record UpdateResourceDto
    {
        public long? HouseId { get; set; } = -1;
        public string? Name { get; set; } = null;
        public required string UserId { get; init; }
    }
}