namespace HouseInv.Api.Models.Entities.Notes
{
    public record NoteDto
    {
        public required Guid Id { get; init; }
        public required string NoteValue { get; init; }
        public required DateTimeOffset CreatedDate { get; init; }
        public required DateTimeOffset ModifiedDate { get; init; }
        public required string CreatedUser { get; init; }
        public required string ModifiedUser { get; init; }
    }
}