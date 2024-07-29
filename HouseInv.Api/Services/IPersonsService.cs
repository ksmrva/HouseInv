using ErrorOr;
using HouseInv.Api.Models.Dtos.Persons;

namespace HouseInv.Api.Services
{
    public interface IPersonsService
    {
        public Task<ErrorOr<PersonDto>> CreatePersonAsync(CreatePersonDto createPersonDto);

        public Task<ErrorOr<PersonDto>> GetPersonAsync(long personId);

        public Task<ErrorOr<List<PersonDto>>> GetPersonsAsync();

        public Task<ErrorOr<Updated>> UpdatePersonAsync(long personId, UpdatePersonDto updatePersonDto);

        public Task<ErrorOr<Deleted>> DeletePersonAsync(long personId);
    }
}