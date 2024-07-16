namespace HouseInv.Models.Dtos.Resources.Personal.Appliance
{
    public record ApplianceDto
    {
        public required long Id { get; init; }
        public required long PersonalResourceId { get; init; }
        public required string Brand { get; init; }
        public required decimal Price { get; init; }
        public required DateTime PurchaseDate { get; init; }
        public required DateTime InstallationDate { get; init; }
        public required DateTime CreatedDate { get; init; }
        public required DateTime ModifiedDate { get; init; }
        public required string CreatedUser { get; init; }
        public required string ModifiedUser { get; init; }
    }
}