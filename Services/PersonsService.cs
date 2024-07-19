using ErrorOr;
using HouseInv.Errors;
using HouseInv.Models.Dtos.Persons;
using HouseInv.Models.Entities.Persons;
using HouseInv.Repositories;

namespace HouseInv.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public PersonsService(HouseInvDbContext houseInvDbContext)
        {
            this.houseInvDbContext = houseInvDbContext;
        }

        public async Task<ErrorOr<PersonDto>> CreatePersonAsync(CreatePersonDto createPersonDto)
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
            return person.AsDto();
        }

        public async Task<ErrorOr<PersonDto>> GetPersonAsync(long personId)
        {
            ErrorOr<PersonDto> result;
            Person person = await houseInvDbContext.Person.FindAsync(personId);
            if (person != null)
            {
                result = person.AsDto();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<List<PersonDto>>> GetPersonsAsync()
        {
            ErrorOr<List<PersonDto>> result;
            IEnumerable<Person> personsResult = houseInvDbContext.Person;
            if (personsResult != null && personsResult.Any())
            {
                IEnumerable<PersonDto> personDtos = personsResult.Select(person => person.AsDto());
                result = personDtos.ToList();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<Updated>> UpdatePersonAsync(long personId, UpdatePersonDto updatePersonDto)
        {
            ErrorOr<Updated> updateResult;
            Person existingPerson = await houseInvDbContext.Person.FindAsync(personId);
            if (existingPerson is null)
            {
                updateResult = DataErrors.DataNotFound;
            }
            else
            {
                if (updatePersonDto.FirstName is null || updatePersonDto.FirstName.Length == 0)
                {
                    updatePersonDto.FirstName = existingPerson.FirstName;
                }
                if (updatePersonDto.LastName is null || updatePersonDto.LastName.Length == 0)
                {
                    updatePersonDto.LastName = existingPerson.LastName;
                }

                Person updatedPerson = existingPerson with
                {
                    FirstName = updatePersonDto.FirstName,
                    LastName = updatePersonDto.LastName,
                    // Adding the CreatedDate here to get around the DateTime needing to be converted to UTC
                    CreatedDate = existingPerson.CreatedDate.ToUniversalTime(),
                    ModifiedDate = DateTime.UtcNow.ToUniversalTime(),
                    ModifiedUser = updatePersonDto.UserId
                };
                houseInvDbContext.Detach(existingPerson);
                houseInvDbContext.Person.Update(updatedPerson);
                await houseInvDbContext.SaveChangesAsync();
                updateResult = Result.Updated;
            }
            return updateResult;
        }

        public async Task<ErrorOr<Deleted>> DeletePersonAsync(long personId)
        {
            ErrorOr<Deleted> deleteResult;
            Person existingPerson = await houseInvDbContext.Person.FindAsync(personId);
            if (existingPerson is null)
            {
                deleteResult = DataErrors.DataNotFound;
            }
            else
            {
                houseInvDbContext.Person.Remove(existingPerson);
                await houseInvDbContext.SaveChangesAsync();
                deleteResult = Result.Deleted;
            }
            return deleteResult;
        }
    }
}