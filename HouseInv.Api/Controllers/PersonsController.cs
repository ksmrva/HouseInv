using ErrorOr;
using HouseInv.Api.Models.Dtos.Persons;
using HouseInv.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Api.Controllers
{
    public class PersonsController : ApiController
    {
        private readonly IPersonsService personsService;

        public PersonsController(IPersonsService personsService)
        {
            this.personsService = personsService;
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePersonAsync(CreatePersonDto createPersonDto)
        {
            ErrorOr<PersonDto> errorOrPersonDto = await personsService.CreatePersonAsync(createPersonDto);

            return errorOrPersonDto.Match(
                personDto => Ok(CreatedAtAction(nameof(GetPersonAsync), new { id = personDto.Id }, personDto)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{personId}")]
        public async Task<ActionResult<PersonDto>> GetPersonAsync(long personId)
        {
            ErrorOr<PersonDto> errorOrPersonDto = await personsService.GetPersonAsync(personId);

            return errorOrPersonDto.Match(
                personDto => Ok(personDto),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonDto>>> GetPersonsAsync()
        {
            ErrorOr<List<PersonDto>> errorOrPersonDtos = await personsService.GetPersonsAsync();

            return errorOrPersonDtos.Match(
                personDtos => Ok(personDtos),
                errors => Problem(errors)
            );
        }

        [HttpPut("{personId}")]
        public async Task<ActionResult> UpdatePersonAsync(long personId, UpdatePersonDto updatePersonDto)
        {
            ErrorOr<Updated> errorOrUpdated = await personsService.UpdatePersonAsync(personId, updatePersonDto);

            return errorOrUpdated.Match(
                updated => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{personId}")]
        public async Task<ActionResult> DeletePersonAsync(long personId)
        {
            ErrorOr<Deleted> errorOrDeleted = await personsService.DeletePersonAsync(personId);

            return errorOrDeleted.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}