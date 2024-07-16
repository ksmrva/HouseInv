namespace HouseInv.Models.Dtos.Resources.Personal.Appliance
{
    public record UpdateApplianceDto
    {
        public required long PersonalResourceId { get; init; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public decimal? Price { get; set; }
        public required string UserId { get; init; }
    }
}