namespace HouseInv.Models.Dtos.Resources.Personal.Appliance
{
    public record CreatePersonalResourceDto
    {
        public required long ResourceId { get; init; }
        public required long TenantId { get; init; }
        public required string UserId { get; init; }
    }
}