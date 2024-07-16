namespace HouseInv.Models.Entities.Notes
{
    public record Note
    {
        public Guid Id { get; init; }
        public required string NoteValue { get; init; }
        public required DateTimeOffset CreatedDate { get; init; }
        public required DateTimeOffset ModifiedDate { get; init; }
        public required string CreatedUser { get; init; }
        public required string ModifiedUser { get; init; }
    }
}