using HouseInv.Api.Models.Entities.Notes;
using Task = System.Threading.Tasks.Task;

namespace HouseInv.Api.Repositories
{
    public interface IAsyncNotesRepository
    {
        Task CreateNoteAsync(Note note);
        Task<Note> GetNoteAsync(Guid noteId);
        Task<IEnumerable<Note>> GetNotesAsync();
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(Guid noteId);
    }
}