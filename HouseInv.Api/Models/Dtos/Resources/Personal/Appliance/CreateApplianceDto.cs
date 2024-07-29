namespace HouseInv.Api.Models.Dtos.Resources.Personal.Appliance
{
    public record CreateApplianceDto
    {
        public required long HouseId { get; init; }
        public required long TenantId { get; init; }
        public required string Name { get; init; }
        public required string Brand { get; init; }
        public required decimal Price { get; init; }
        public required DateTime PurchaseDate { get; init; }
        public required DateTime InstallationDate { get; init; }
        public required string UserId { get; init; }
    }
}