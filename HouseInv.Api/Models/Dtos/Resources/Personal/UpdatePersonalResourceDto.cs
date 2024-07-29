namespace HouseInv.Api.Models.Dtos.Resources.Personal.Appliance
{
    public record UpdatePersonalResourceDto
    {
        public long? ResourceId { get; set; } = -1;
        public long? TenantId { get; set; } = -1;
        public required string UserId { get; set; }
    }
}