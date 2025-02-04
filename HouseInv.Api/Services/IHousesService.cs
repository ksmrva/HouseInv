using ErrorOr;
using HouseInv.Api.Models.Dtos.Houses;

namespace HouseInv.Api.Services
{
    public interface IHousesService
    {
        public Task<ErrorOr<HouseDto>> CreateHouseAsync(CreateHouseDto createHouseDto);

        public Task<ErrorOr<HouseDto>> GetHouseAsync(long houseId);

        public Task<ErrorOr<List<HouseDto>>> GetHousesAsync();

        public Task<ErrorOr<Updated>> UpdateHouseAsync(long houseId, UpdateHouseDto updateHouseDto);

        public Task<ErrorOr<Deleted>> DeleteHouseAsync(long houseId);
    }
}