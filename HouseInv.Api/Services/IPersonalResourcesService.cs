using ErrorOr;
using HouseInv.Api.Models.Dtos.Resources.Personal;
using HouseInv.Api.Models.Dtos.Resources.Personal.Appliance;

namespace HouseInv.Api.Services
{
    public interface IPersonalResourcesService
    {
        public Task<ErrorOr<PersonalResourceDto>> CreatePersonalResourceAsync(CreatePersonalResourceDto createPersonalResourceDto);

        public Task<ErrorOr<PersonalResourceDto>> GetPersonalResourceAsync(long personalResourceId);

        public Task<ErrorOr<List<PersonalResourceDto>>> GetPersonalResourcesAsync();

        public Task<ErrorOr<Updated>> UpdatePersonalResourceAsync(long personalResourceId, UpdatePersonalResourceDto updatePersonalResourceDto);

        public Task<ErrorOr<Deleted>> DeletePersonalResourceAsync(long personalResourceId);
    }
}