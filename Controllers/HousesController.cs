using HouseInv.Models.Dtos.Houses;
using HouseInv.Models.Entities.Houses;
using HouseInv.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Controllers
{
    [ApiController]
    [Route("houses")]
    public class HousesController : ControllerBase
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public HousesController(HouseInvDbContext houseInvDbContext) 
        {
            // this.houseRepository = houseRepository;
            this.houseInvDbContext = houseInvDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<HouseDto>> CreateHouseAsync(CreateHouseDto createHouseDto)
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

            return CreatedAtAction(nameof(GetHouseAsync), new { id = house.Id }, house.AsDto() );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HouseDto>> GetHouseAsync(long id)
        {
            ActionResult<HouseDto> actionResult;
            House houseResult = await houseInvDbContext.House.FindAsync(id);
            if (houseResult == null) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(houseResult.AsDto());
            }
            return actionResult;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseDto>>> GetHousesAsync()
        {
            ActionResult<IEnumerable<HouseDto>> actionResult;
            IEnumerable<House> housesResult = houseInvDbContext.House;
            if (housesResult == null || !(housesResult.Any())) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(housesResult.Select(house => house.AsDto()));
            }
            return await Task.FromResult(actionResult);
        }

        [HttpPut("{houseId}")]
        public async Task<ActionResult> UpdateHouseAsync(long houseId, UpdateHouseDto updatedHouseDto)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // House existingHouse = await houseInvDbContext.House.FindAsync(houseId);
            
            // if(updatedHouseDto.UserId is null || updatedHouseDto.UserId.Length == 0) {
            //     //throw new Exception("User ID must be defined");
            // }
            // if(existingHouse is null) {
            //     result = NotFound();
            // } else {
                
            //     if(updatedHouseDto.Name is null || updatedHouseDto.Name.Length == 0) {
            //         updatedHouseDto.Name = existingHouse.Name;
            //     }
            //     if(updatedHouseDto.Address1 is null || updatedHouseDto.Address1.Length == 0) {
            //         updatedHouseDto.Address1 = existingHouse.Address1;
            //     }
            //     if(updatedHouseDto.Address2 is null || updatedHouseDto.Address2.Length == 0) {
            //         updatedHouseDto.Address2 = existingHouse.Address2;
            //     }
            //     if(updatedHouseDto.City is null || updatedHouseDto.City.Length == 0) {
            //         updatedHouseDto.City = existingHouse.City;
            //     }
            //     if(updatedHouseDto.State is null || updatedHouseDto.State.Length == 0) {
            //         updatedHouseDto.State = existingHouse.State;
            //     }
            //     if(updatedHouseDto.Zip is null || updatedHouseDto.Zip.Length == 0) {
            //         updatedHouseDto.Zip = existingHouse.Zip;
            //     }

            //     House updatedHouse = existingHouse with {
            //         Name = updatedHouseDto.Name,
            //         Address1 = updatedHouseDto.Address1,
            //         Address2 = updatedHouseDto.Address2,
            //         City = updatedHouseDto.City,
            //         State = updatedHouseDto.State,
            //         Zip = updatedHouseDto.Zip,
            //         ModifiedDate = DateTime.UtcNow,
            //         ModifiedUser = updatedHouseDto.UserId
            //     };
            //     await houseRepository.UpdateHouseAsync(updatedHouse);
            //     result = NoContent();
            // }
            // return result;
        }

        [HttpDelete("{houseId}")]
        public async Task<ActionResult> DeleteHouseAsync(long houseId)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // House existingHouse = await houseRepository.GetHouseAsync(houseId);
            // if(existingHouse is null) {
            //     result = NotFound();
            // } else {
            //     await houseRepository.DeleteHouseAsync(houseId);
            //     result = NoContent();
            // }
            // return result;
        }
    }
}