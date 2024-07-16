namespace HouseInv.Repositories.Configurations.Schema;

public record HouseInvDbSchema : IDbContextSchema
{
    public required string Schema { get; init; }
}