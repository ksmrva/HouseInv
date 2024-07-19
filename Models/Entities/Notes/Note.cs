namespace HouseInv.Models.Entities.Notes
{
    public class Note
    {
        public Guid Id { get; set; }
        public required string NoteValue { get; set; }
        public required DateTimeOffset CreatedDate { get; set; }
        public required DateTimeOffset ModifiedDate { get; set; }
        public required string CreatedUser { get; set; }
        public required string ModifiedUser { get; set; }
    }
}