namespace HouseInv.Models.Entities.Notes
{
    public record UpdateNoteDto
    {
        public required string NoteValue { get; set; }
        public required string UserId { get; set; }
    }
}