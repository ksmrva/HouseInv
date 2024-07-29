namespace HouseInv.Api.Models.Dtos.Houses
{
    public record UpdateHouseDto
    {
        public string? Name { get; set; } = null;
        public string? Address1 { get; set; } = null;
        public string? Address2 { get; set; } = null;
        public string? City { get; set; } = null;
        public string? State { get; set; } = null;
        public string? Zip { get; set; } = null;
        public long? OwnerId { get; set; } = -1;
        public required string UserId { get; init; }
    }
}