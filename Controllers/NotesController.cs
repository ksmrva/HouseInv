using ErrorOr;
using HouseInv.Models.Entities.Notes;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Controllers
{
    public class NotesController : ApiController
    {
        private readonly INotesService notesService;

        public NotesController(INotesService notesService)
        {
            this.notesService = notesService;
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNoteAsync(CreateNoteDto createNoteDto)
        {
            ErrorOr<NoteDto> errorOrNoteDto = await notesService.CreateNoteAsync(createNoteDto);

            return errorOrNoteDto.Match(
                noteDto => Ok(CreatedAtAction(nameof(GetNoteAsync), new { id = noteDto.Id }, noteDto)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult<NoteDto>> GetNoteAsync(Guid noteId)
        {
            ErrorOr<NoteDto> errorOrNoteDto = await notesService.GetNoteAsync(noteId);

            return errorOrNoteDto.Match(
                noteDto => Ok(noteDto),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteDto>>> GetNotesAsync()
        {
            ErrorOr<List<NoteDto>> errorOrNoteDtos = await notesService.GetNotesAsync();

            return errorOrNoteDtos.Match(
                noteDtos => Ok(noteDtos),
                errors => Problem(errors)
            );
        }

        [HttpPut("{noteId}")]
        public async Task<ActionResult> UpdateNoteAsync(Guid noteId, UpdateNoteDto updateNoteDto)
        {
            ErrorOr<Updated> errorOrUpdated = await notesService.UpdateNoteAsync(noteId, updateNoteDto);

            return errorOrUpdated.Match(
                updated => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult> DeleteNoteAsync(Guid noteId)
        {
            ErrorOr<Deleted> errorOrDeleted = await notesService.DeleteNoteAsync(noteId);

            return errorOrDeleted.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}