namespace HouseInv.Api.Models.Entities.Notes
{
    public record UpdateNoteDto
    {
        public string? NoteValue { get; set; } = null;
        public required string UserId { get; set; }
    }
}