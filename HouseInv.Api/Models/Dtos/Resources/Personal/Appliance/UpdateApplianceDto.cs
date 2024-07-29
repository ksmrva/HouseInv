namespace HouseInv.Api.Models.Dtos.Resources.Personal.Appliance
{
    public record UpdateApplianceDto
    {
        public string? Brand { get; set; } = null;
        public decimal? Price { get; set; } = -1;
        public DateTime? PurchaseDate { get; set; } = null;
        public DateTime? InstallationDate { get; set; } = null;
        public required string UserId { get; init; }
    }
}