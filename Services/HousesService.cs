using ErrorOr;
using HouseInv.Errors;
using HouseInv.Models.Dtos.Houses;
using HouseInv.Models.Entities.Houses;
using HouseInv.Repositories;

namespace HouseInv.Services
{
    public class HousesService : IHousesService
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public HousesService(HouseInvDbContext houseInvDbContext)
        {
            this.houseInvDbContext = houseInvDbContext;
        }

        public async Task<ErrorOr<HouseDto>> CreateHouseAsync(CreateHouseDto createHouseDto)
        {
            var utcNowValue = DateTime.UtcNow;
            House house = new()
            {
                Name = createHouseDto.Name,
                Address1 = createHouseDto.Address1,
                Address2 = createHouseDto.Address2,
                City = createHouseDto.City,
                State = createHouseDto.State,
                Zip = createHouseDto.Zip,
                OwnerId = createHouseDto.OwnerId,
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createHouseDto.UserId,
                ModifiedUser = createHouseDto.UserId
            };
            await houseInvDbContext.House.AddAsync(house);
            await houseInvDbContext.SaveChangesAsync();
            return house.AsDto();
        }

        public async Task<ErrorOr<HouseDto>> GetHouseAsync(long houseId)
        {
            ErrorOr<HouseDto> result;
            House house = await houseInvDbContext.House.FindAsync(houseId);
            if (house != null)
            {
                result = house.AsDto();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<List<HouseDto>>> GetHousesAsync()
        {
            ErrorOr<List<HouseDto>> result;
            IEnumerable<House> housesResult = houseInvDbContext.House;
            if (housesResult != null && housesResult.Any())
            {
                IEnumerable<HouseDto> houseDtos = housesResult.Select(house => house.AsDto());
                result = houseDtos.ToList();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<Updated>> UpdateHouseAsync(long houseId, UpdateHouseDto updateHouseDto)
        {
            ErrorOr<Updated> updateResult;
            House existingHouse = await houseInvDbContext.House.FindAsync(houseId);
            if (existingHouse is null)
            {
                updateResult = DataErrors.DataNotFound;
            }
            else
            {
                if (updateHouseDto.Name is null || updateHouseDto.Name.Length == 0)
                {
                    updateHouseDto.Name = existingHouse.Name;
                }
                if (updateHouseDto.Address1 is null || updateHouseDto.Address1.Length == 0)
                {
                    updateHouseDto.Address1 = existingHouse.Address1;
                }
                if (updateHouseDto.Address2 is null || updateHouseDto.Address2.Length == 0)
                {
                    updateHouseDto.Address2 = existingHouse.Address2;
                }
                if (updateHouseDto.City is null || updateHouseDto.City.Length == 0)
                {
                    updateHouseDto.City = existingHouse.City;
                }
                if (updateHouseDto.State is null || updateHouseDto.State.Length == 0)
                {
                    updateHouseDto.State = existingHouse.State;
                }
                if (updateHouseDto.Zip is null || updateHouseDto.Zip.Length == 0)
                {
                    updateHouseDto.Zip = existingHouse.Zip;
                }

                House updatedHouse = existingHouse with
                {
                    Name = updateHouseDto.Name,
                    Address1 = updateHouseDto.Address1,
                    Address2 = updateHouseDto.Address2,
                    City = updateHouseDto.City,
                    State = updateHouseDto.State,
                    Zip = updateHouseDto.Zip,
                    OwnerId = (long)updateHouseDto.OwnerId,
                    // Adding the CreatedDate here to get around the DateTime needing to be converted to UTC
                    CreatedDate = existingHouse.CreatedDate.ToUniversalTime(),
                    ModifiedDate = DateTime.UtcNow.ToUniversalTime(),
                    ModifiedUser = updateHouseDto.UserId
                };
                houseInvDbContext.Detach(existingHouse);
                houseInvDbContext.House.Update(updatedHouse);
                await houseInvDbContext.SaveChangesAsync();
                updateResult = Result.Updated;
            }
            return updateResult;
        }

        public async Task<ErrorOr<Deleted>> DeleteHouseAsync(long houseId)
        {
            ErrorOr<Deleted> deleteResult;
            House existingHouse = await houseInvDbContext.House.FindAsync(houseId);
            if (existingHouse is null)
            {
                deleteResult = DataErrors.DataNotFound;
            }
            else
            {
                houseInvDbContext.House.Remove(existingHouse);
                await houseInvDbContext.SaveChangesAsync();
                deleteResult = Result.Deleted;
            }
            return deleteResult;
        }
    }
}