namespace HouseInv.Models.Entities.Notes
{
    public record CreateNoteDto
    {
        public required string NoteValue { get; set; }
        public required string UserId { get; set; }
    }
}