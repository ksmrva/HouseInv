namespace HouseInv.Models.Dtos.Tenants
{
    public record UpdateTenantDto
    {
        public long? HouseId { get; set; } = -1;
        public long? PersonId { get; set; } = -1;
        public required string UserId { get; set; }
    }
}