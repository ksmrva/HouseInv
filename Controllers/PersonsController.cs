using HouseInv.Models.Dtos.Persons;
using HouseInv.Models.Entities.Persons;
using HouseInv.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : ControllerBase
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public PersonsController(HouseInvDbContext houseInvDbContext) 
        {
            this.houseInvDbContext = houseInvDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePersonAsync(CreatePersonDto createPersonDto)
        {
            var utcNowValue = DateTime.UtcNow;
            Person person = new()
            {
                FirstName = createPersonDto.FirstName,
                LastName = createPersonDto.LastName,
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createPersonDto.UserId,
                ModifiedUser = createPersonDto.UserId
            };

            await houseInvDbContext.Person.AddAsync(person);
            await houseInvDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersonAsync), new { personId = person.Id }, person.AsDto() );
        }

        [HttpGet("{personId}")]
        public async Task<ActionResult<PersonDto>> GetPersonAsync(long personId)
        {
            ActionResult<PersonDto> actionResult;
            Person personResult = await houseInvDbContext.Person.FindAsync(personId);
            if (personResult == null) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(personResult.AsDto());
            }
            return actionResult;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersonsAsync()
        {
            ActionResult<IEnumerable<PersonDto>> actionResult;
            IEnumerable<Person> personsResult = houseInvDbContext.Person;
            if (personsResult == null || !(personsResult.Any())) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(personsResult.Select(person => person.AsDto()));
            }
            return await Task.FromResult(actionResult);
        }

        [HttpPut("{personId}")]
        public async Task<ActionResult> UpdatePersonAsync(long personId, UpdatePersonDto updatePersonDto)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // Person existingPerson = await personRepository.GetPersonAsync(personId);
            // if(existingPerson is null) {
            //     result = NotFound();
            // } else {

            //     if(updatePersonDto.FirstName is null || updatePersonDto.FirstName.Length == 0) {
            //         updatePersonDto.FirstName = existingPerson.FirstName;
            //     }
            //     if(updatePersonDto.LastName is null || updatePersonDto.LastName.Length == 0) {
            //         updatePersonDto.LastName = existingPerson.LastName;
            //     }
                
            //     Person updatedPerson = existingPerson with {
            //         FirstName = updatePersonDto.FirstName,
            //         LastName = updatePersonDto.LastName,
            //         ModifiedDate = DateTimeOffset.UtcNow,
            //         ModifiedUser = updatePersonDto.UserId
            //     };
            //     await personRepository.UpdatePersonAsync(updatedPerson);
            //     result = NoContent();
            // }
            // return result;
        }

        [HttpDelete("{personId}")]
        public async Task<ActionResult> DeletePersonAsync(long personId)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // Person existingPerson = await personRepository.GetPersonAsync(personId);
            // if(existingPerson is null) {
            //     result = NotFound();
            // } else {
            //     await personRepository.DeletePersonAsync(personId);
            //     result = NoContent();
            // }
            // return result;
        }
    }
}