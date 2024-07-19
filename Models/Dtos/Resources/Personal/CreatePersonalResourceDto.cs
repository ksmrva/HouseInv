namespace HouseInv.Models.Dtos.Resources.Personal.Appliance
{
    public record CreatePersonalResourceDto
    {
        public required string Name { get; init; }
        public required long HouseId { get; init; }
        public required long TenantId { get; init; }
        public required string UserId { get; init; }
    }
}