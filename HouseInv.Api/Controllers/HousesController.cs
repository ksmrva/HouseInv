using ErrorOr;
using HouseInv.Api.Models.Dtos.Houses;
using HouseInv.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Api.Controllers
{
    public class HousesController : ApiController
    {
        private readonly IHousesService housesService;

        public HousesController(IHousesService housesService)
        {
            this.housesService = housesService;
        }

        [HttpPost]
        public async Task<ActionResult<HouseDto>> CreateHouseAsync(CreateHouseDto createHouseDto)
        {
            ErrorOr<HouseDto> errorOrResourceDto = await housesService.CreateHouseAsync(createHouseDto);

            return errorOrResourceDto.Match(
                resourceDto => Ok(CreatedAtAction(nameof(GetHouseAsync), new { id = resourceDto.Id }, resourceDto)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{houseId}")]
        public async Task<ActionResult<HouseDto>> GetHouseAsync(long houseId)
        {
            ErrorOr<HouseDto> errorOrResourceDto = await housesService.GetHouseAsync(houseId);

            return errorOrResourceDto.Match(
                resourceDto => Ok(resourceDto),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseDto>>> GetHousesAsync()
        {
            ErrorOr<List<HouseDto>> errorOrResourceDtos = await housesService.GetHousesAsync();

            return errorOrResourceDtos.Match(
                resourceDtos => Ok(resourceDtos),
                errors => Problem(errors)
            );
        }

        [HttpPut("{houseId}")]
        public async Task<ActionResult> UpdateHouseAsync(long houseId, UpdateHouseDto updatedHouseDto)
        {
            ErrorOr<Updated> errorOrUpdated = await housesService.UpdateHouseAsync(houseId, updatedHouseDto);

            return errorOrUpdated.Match(
                updated => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{houseId}")]
        public async Task<ActionResult> DeleteHouseAsync(long houseId)
        {
            ErrorOr<Deleted> errorOrDeleted = await housesService.DeleteHouseAsync(houseId);

            return errorOrDeleted.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}