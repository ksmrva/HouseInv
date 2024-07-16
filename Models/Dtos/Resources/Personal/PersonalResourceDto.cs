namespace HouseInv.Models.Dtos.Resources.Personal
{
    public record PersonalResourceDto
    {
        public required long Id { get; init; }
        public required long ResourceId { get; init; }
        public required long TenantId { get; init; }
        public required DateTime CreatedDate { get; init; }
        public required DateTime ModifiedDate { get; init; }
        public required string CreatedUser { get; init; }
        public required string ModifiedUser { get; init; }
    }
}