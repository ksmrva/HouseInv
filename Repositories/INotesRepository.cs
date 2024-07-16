using HouseInv.Models.Entities.Notes;

namespace HouseInv.Repositories
{
    public interface INotesRepository
    {
        void CreateNote(Note note);
        Note GetNote(Guid noteId);
        IEnumerable<Note> GetNotes();
        void UpdateNote(Note note);
        void DeleteNote(Guid noteId);
    }
}