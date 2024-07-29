using ErrorOr;
using HouseInv.Api.Errors;
using HouseInv.Api.Models.Dtos.Houses;
using HouseInv.Api.Models.Entities.Houses;
using HouseInv.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HouseInv.Api.Services
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
            ErrorOr<House> createHouseResult = House.FromCreateDto(createHouseDto);

            ErrorOr<HouseDto> result;
            if (createHouseResult.IsError)
            {
                result = createHouseResult.Errors;
            }
            else
            {
                House house = createHouseResult.Value;
                await houseInvDbContext.House.AddAsync(house);
                await houseInvDbContext.SaveChangesAsync();
                result = house.AsDto();
            }
            return result;
        }

        public async Task<ErrorOr<HouseDto>> GetHouseAsync(long houseId)
        {
            ErrorOr<HouseDto> result;
            House house = await houseInvDbContext.House
                                                 .Include(h => h.Owner)
                                                 .FirstOrDefaultAsync(h => h.Id == houseId);
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

        public async Task<ErrorOr<HouseDto>> GetHouseAsync2(long houseId)
        {
            ErrorOr<HouseDto> result;
            House house = await houseInvDbContext.House.FindAsync(houseId);
            if (house != null)
            {
                houseInvDbContext.Entry(house).Reference(h => h.Owner).Load();
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
                if (updateHouseDto.OwnerId is null || updateHouseDto.OwnerId <= 0)
                {
                    updateHouseDto.OwnerId = existingHouse.OwnerId;
                }

                ErrorOr<House> updatedHouseResult = House.Create(existingHouse.Id,
                                                                 updateHouseDto.Name,
                                                                 updateHouseDto.Address1,
                                                                 updateHouseDto.Address2,
                                                                 updateHouseDto.City,
                                                                 updateHouseDto.State,
                                                                 updateHouseDto.Zip,
                                                                 (long)updateHouseDto.OwnerId,
                                                                 DateTime.UtcNow.ToUniversalTime(),
                                                                 existingHouse.CreatedDate.ToUniversalTime(),
                                                                 existingHouse.CreatedUser,
                                                                 updateHouseDto.UserId);
                if (updatedHouseResult.IsError)
                {
                    updateResult = updatedHouseResult.Errors;
                }
                else
                {
                    houseInvDbContext.Detach(existingHouse);
                    houseInvDbContext.House.Update(updatedHouseResult.Value);
                    await houseInvDbContext.SaveChangesAsync();
                    updateResult = Result.Updated;
                }
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

        // private HouseDto FinalizeChangesFromUpdatedAndExisting()
        // {
        //     HouseDto finalizedHouseChanges
        //     if (updateHouseDto.Name is null || updateHouseDto.Name.Length == 0)
        //         {
        //             updateHouseDto.Name = existingHouse.Name;
        //         }
        //         if (updateHouseDto.Address1 is null || updateHouseDto.Address1.Length == 0)
        //         {
        //             updateHouseDto.Address1 = existingHouse.Address1;
        //         }
        //         if (updateHouseDto.Address2 is null || updateHouseDto.Address2.Length == 0)
        //         {
        //             updateHouseDto.Address2 = existingHouse.Address2;
        //         }
        //         if (updateHouseDto.City is null || updateHouseDto.City.Length == 0)
        //         {
        //             updateHouseDto.City = existingHouse.City;
        //         }
        //         if (updateHouseDto.State is null || updateHouseDto.State.Length == 0)
        //         {
        //             updateHouseDto.State = existingHouse.State;
        //         }
        //         if (updateHouseDto.Zip is null || updateHouseDto.Zip.Length == 0)
        //         {
        //             updateHouseDto.Zip = existingHouse.Zip;
        //         }
        //         if (updateHouseDto.OwnerId is null || updateHouseDto.OwnerId <= 0)
        //         {
        //             updateHouseDto.OwnerId = existingHouse.OwnerId;
        //         }
        // }
    }
}