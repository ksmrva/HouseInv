using ErrorOr;
using HouseInv.Errors;
using HouseInv.Models.Entities.Notes;
using HouseInv.Repositories;

namespace HouseInv.Services
{
    public class NotesService : INotesService
    {
        private readonly IAsyncNotesRepository noteRepository;

        public NotesService(IAsyncNotesRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public async Task<ErrorOr<NoteDto>> CreateNoteAsync(CreateNoteDto createNoteDto)
        {
            var utcNowValue = DateTimeOffset.UtcNow;
            Note note = new()
            {
                Id = Guid.NewGuid(),
                NoteValue = createNoteDto.NoteValue,
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createNoteDto.UserId,
                ModifiedUser = createNoteDto.UserId
            };
            await noteRepository.CreateNoteAsync(note);
            return note.AsDto();
        }

        public async Task<ErrorOr<NoteDto>> GetNoteAsync(Guid noteId)
        {
            ErrorOr<NoteDto> result;
            Note note = await noteRepository.GetNoteAsync(noteId);
            if (note != null)
            {
                result = note.AsDto();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return result;
        }

        public async Task<ErrorOr<List<NoteDto>>> GetNotesAsync()
        {
            ErrorOr<List<NoteDto>> result;
            IEnumerable<Note> notesResult = await noteRepository.GetNotesAsync();
            if (notesResult != null && notesResult.Any())
            {
                IEnumerable<NoteDto> notesDtos = notesResult.Select(note => note.AsDto());
                result = notesDtos.ToList();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result); ;
        }

        public async Task<ErrorOr<Updated>> UpdateNoteAsync(Guid noteId, UpdateNoteDto updateNoteDto)
        {
            ErrorOr<Updated> updateResult;
            Note existingNote = await noteRepository.GetNoteAsync(noteId);
            if (existingNote is null)
            {
                updateResult = DataErrors.DataNotFound;
            }
            else
            {
                if (updateNoteDto.NoteValue is null || updateNoteDto.NoteValue.Length == 0)
                {
                    updateNoteDto.NoteValue = existingNote.NoteValue;
                }

                Note updatedNote = new()
                {
                    Id = existingNote.Id,
                    CreatedDate = existingNote.CreatedDate.ToUniversalTime(),
                    CreatedUser = existingNote.CreatedUser,

                    NoteValue = updateNoteDto.NoteValue,

                    ModifiedDate = DateTimeOffset.UtcNow,
                    ModifiedUser = updateNoteDto.UserId
                };
                await noteRepository.UpdateNoteAsync(updatedNote);
                updateResult = Result.Updated;
            }
            return updateResult;
        }

        public async Task<ErrorOr<Deleted>> DeleteNoteAsync(Guid noteId)
        {
            ErrorOr<Deleted> deleteResult;
            Note existingNote = await noteRepository.GetNoteAsync(noteId);
            if (existingNote is null)
            {
                deleteResult = DataErrors.DataNotFound;
            }
            else
            {
                await noteRepository.DeleteNoteAsync(noteId);
                deleteResult = Result.Deleted;
            }
            return deleteResult;
        }
    }
}