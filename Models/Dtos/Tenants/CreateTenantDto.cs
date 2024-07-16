namespace HouseInv.Models.Dtos.Tenants
{
    public record CreateTenantDto
    {
        public required long HouseId { get; init; }
        public required long PersonId { get; init; }
        public required string UserId { get; init; }
    }
}