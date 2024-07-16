using HouseInv.Models.Entities.Notes;
using HouseInv.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Controllers
{
    [ApiController]
    [Route("notes")]
    public class NotesController : ControllerBase
    {
        private readonly IAsyncNotesRepository noteRepository;

        public NotesController(IAsyncNotesRepository noteRepository) 
        {
            this.noteRepository = noteRepository;
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNoteAsync(CreateNoteDto createNoteDto)
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

            return CreatedAtAction(nameof(GetNoteAsync), new { id = note.Id }, note.AsDto() );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNoteAsync(Guid noteId)
        {
            ActionResult<NoteDto> actionResult;
            Note noteResult = await noteRepository.GetNoteAsync(noteId);
            if (noteResult == null) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(noteResult.AsDto());
            }
            return actionResult;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotesAsync()
        {
            ActionResult<IEnumerable<NoteDto>> actionResult;
            IEnumerable<Note> notesResult = await noteRepository.GetNotesAsync();
            if (notesResult == null || !(notesResult.Any())) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(notesResult.Select(note => note.AsDto()));
            }
            return actionResult;
        }

        [HttpPut("{noteId}")]
        public async Task<ActionResult> UpdateNoteAsync(Guid noteId, UpdateNoteDto updateNoteDto)
        {
            ActionResult result;
            Note existingNote = await noteRepository.GetNoteAsync(noteId);
            if(existingNote is null) {
                result = NotFound();
            } else {
                if(updateNoteDto.NoteValue is null || updateNoteDto.NoteValue.Length == 0) {
                    updateNoteDto.NoteValue = existingNote.NoteValue;
                }
                
                Note updatedNote = existingNote with {
                    NoteValue = updateNoteDto.NoteValue,
                    ModifiedDate = DateTimeOffset.UtcNow,
                    ModifiedUser = updateNoteDto.UserId
                };
                await noteRepository.UpdateNoteAsync(updatedNote);
                result = NoContent();
            }
            return result;
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult> DeleteNoteAsync(Guid noteId)
        {
            ActionResult result;
            Note existingNote = await noteRepository.GetNoteAsync(noteId);
            if(existingNote is null) {
                result = NotFound();
            } else {
                await noteRepository.DeleteNoteAsync(noteId);
                result = NoContent();
            }
            return result;
        }
    }
}