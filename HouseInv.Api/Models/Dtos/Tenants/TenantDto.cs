namespace HouseInv.Api.Models.Dtos.Tenants
{
    public record TenantDto
    {
        public required long Id { get; init; }
        public required long HouseId { get; init; }
        public required long PersonId { get; init; }
        public required DateTime CreatedDate { get; init; }
        public required DateTime ModifiedDate { get; init; }
        public required string CreatedUser { get; init; }
        public required string ModifiedUser { get; init; }
    }
}