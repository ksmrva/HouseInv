using ErrorOr;
using HouseInv.Models.Entities.Notes;

public interface INotesService
{
    public Task<ErrorOr<NoteDto>> CreateNoteAsync(CreateNoteDto createNoteDto);

    public Task<ErrorOr<NoteDto>> GetNoteAsync(Guid noteId);

    public Task<ErrorOr<List<NoteDto>>> GetNotesAsync();

    public Task<ErrorOr<Updated>> UpdateNoteAsync(Guid noteId, UpdateNoteDto updateNoteDto);

    public Task<ErrorOr<Deleted>> DeleteNoteAsync(Guid noteId);
}