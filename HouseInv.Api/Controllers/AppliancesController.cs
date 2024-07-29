using ErrorOr;
using HouseInv.Api.Models.Dtos.Resources.Personal.Appliance;
using HouseInv.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Api.Controllers
{
    public class AppliancesController : ApiController
    {
        private readonly IAppliancesService appliancesService;

        public AppliancesController(IAppliancesService appliancesService)
        {
            this.appliancesService = appliancesService;
        }

        [HttpPost]
        public async Task<ActionResult<ApplianceDto>> CreateApplianceAsync(CreateApplianceDto createApplianceDto)
        {
            ErrorOr<ApplianceDto> errorOrApplianceDto = await appliancesService.CreateApplianceAsync(createApplianceDto);

            return errorOrApplianceDto.Match(
                applianceDto => Ok(CreatedAtAction(nameof(GetApplianceAsync), new { id = applianceDto.Id }, applianceDto)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{applianceId}")]
        public async Task<ActionResult<ApplianceDto>> GetApplianceAsync(long applianceId)
        {
            ErrorOr<ApplianceDto> errorOrApplianceDto = await appliancesService.GetApplianceAsync(applianceId);

            return errorOrApplianceDto.Match(
                applianceDto => Ok(applianceDto),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplianceDto>>> GetAppliancesAsync()
        {
            ErrorOr<List<ApplianceDto>> errorOrApplianceDtos = await appliancesService.GetAppliancesAsync();

            return errorOrApplianceDtos.Match(
                applianceDtos => Ok(applianceDtos),
                errors => Problem(errors)
            );
        }

        [HttpPut("{applianceId}")]
        public async Task<ActionResult> UpdateApplianceAsync(long applianceId, UpdateApplianceDto updateApplianceDto)
        {
            ErrorOr<Updated> errorOrUpdated = await appliancesService.UpdateApplianceAsync(applianceId, updateApplianceDto);

            return errorOrUpdated.Match(
                updated => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{applianceId}")]
        public async Task<ActionResult> DeleteApplianceAsync(long applianceId)
        {
            ErrorOr<Deleted> errorOrDeleted = await appliancesService.DeleteApplianceAsync(applianceId);

            return errorOrDeleted.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}