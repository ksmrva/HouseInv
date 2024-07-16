namespace HouseInv.Models.Dtos.Tenants
{
    public record UpdateTenantDto
    {
        public required long HouseId { get; set; }
        public required long PersonId { get; set; }
        public required string UserId { get; set; }
    }
}