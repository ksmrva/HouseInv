using ErrorOr;
using HouseInv.Api.Models.Dtos.Tenants;
using HouseInv.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Api.Controllers
{
    public class TenantsController : ApiController
    {
        private readonly ITenantsService tenantsService;

        public TenantsController(ITenantsService tenantsService)
        {
            this.tenantsService = tenantsService;
        }

        [HttpPost]
        public async Task<ActionResult<TenantDto>> CreateTenantAsync(CreateTenantDto createTenantDto)
        {
            ErrorOr<TenantDto> errorOrTenantDto = await tenantsService.CreateTenantAsync(createTenantDto);

            return errorOrTenantDto.Match(
                tenantDto => Ok(CreatedAtAction(nameof(GetTenantAsync), new { id = tenantDto.Id }, tenantDto)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{tenantId}")]
        public async Task<ActionResult<TenantDto>> GetTenantAsync(long tenantId)
        {
            ErrorOr<TenantDto> errorOrTenantDto = await tenantsService.GetTenantAsync(tenantId);

            return errorOrTenantDto.Match(
                tenantDto => Ok(tenantDto),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<ActionResult<List<TenantDto>>> GetTenantsAsync()
        {
            ErrorOr<List<TenantDto>> errorOrTenantDtos = await tenantsService.GetTenantsAsync();

            return errorOrTenantDtos.Match(
                tenantDtos => Ok(tenantDtos),
                errors => Problem(errors)
            );
        }

        [HttpPut("{tenantId}")]
        public async Task<ActionResult> UpdateTenantAsync(long tenantId, UpdateTenantDto updateTenantDto)
        {
            ErrorOr<Updated> errorOrUpdated = await tenantsService.UpdateTenantAsync(tenantId, updateTenantDto);

            return errorOrUpdated.Match(
                updated => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{tenantId}")]
        public async Task<ActionResult> DeleteTenantAsync(long tenantId)
        {
            ErrorOr<Deleted> errorOrDeleted = await tenantsService.DeleteTenantAsync(tenantId);

            return errorOrDeleted.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}