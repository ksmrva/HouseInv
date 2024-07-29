namespace HouseInv.Api.Repositories.Configurations.Schema;

public record HouseInvDbSchema : IDbContextSchema
{
    public required string Schema { get; init; }
}