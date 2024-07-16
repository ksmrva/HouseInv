using HouseInv.Models.Dtos.Tenants;
using HouseInv.Models.Entities.Tenants;
using HouseInv.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Controllers
{
    [ApiController]
    [Route("tenants")]
    public class TenantsController : ControllerBase
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public TenantsController(HouseInvDbContext houseInvDbContext) 
        {
            this.houseInvDbContext = houseInvDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<TenantDto>> CreateTenantAsync(CreateTenantDto createTenantDto)
        {
            var utcNowValue = DateTime.UtcNow;
            Tenant tenant = new()
            {
                HouseId = createTenantDto.HouseId,
                PersonId = createTenantDto.PersonId,
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createTenantDto.UserId,
                ModifiedUser = createTenantDto.UserId
            };
            await houseInvDbContext.Tenant.AddAsync(tenant);
            await houseInvDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTenantAsync), new { tenantId = tenant.Id }, tenant.AsDto() );
        }

        [HttpGet("{tenantId}")]
        public async Task<ActionResult<TenantDto>> GetTenantAsync(long tenantId)
        {
            ActionResult<TenantDto> actionResult;
            Tenant tenantResult = await houseInvDbContext.Tenant.FindAsync(tenantId);
            if (tenantResult == null) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(tenantResult.AsDto());
            }
            return actionResult;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDto>>> GetTenantsAsync()
        {
            ActionResult<IEnumerable<TenantDto>> actionResult;
            IEnumerable<Tenant> tenantsResult = houseInvDbContext.Tenant;
            if (tenantsResult == null || !(tenantsResult.Any())) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(tenantsResult.Select(tenant => tenant.AsDto()));
            }
            return await Task.FromResult(actionResult);
        }

        [HttpPut("{tenantId}")]
        public async Task<ActionResult> UpdateTenantAsync(long tenantId, UpdateTenantDto updateTenantDto)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // Tenant existingTenant = await tenantRepository.GetTenantAsync(tenantId);
            // if(existingTenant is null) {
            //     result = NotFound();
            // } else {

            //     // if(updateTenantDto.HouseId is null || updateTenantDto.HouseId.Length == 0) {
            //     //     updateTenantDto.HouseId = existingTenant.HouseId;
            //     // }
            //     // if(updateTenantDto.PersonId is null || updateTenantDto.PersonId.Length == 0) {
            //     //     updateTenantDto.PersonId = existingTenant.PersonId;
            //     // }
                
            //     Tenant updatedTenant = existingTenant with {
            //         HouseId = updateTenantDto.HouseId,
            //         PersonId = updateTenantDto.PersonId,
            //         ModifiedDate = DateTime.UtcNow,
            //         ModifiedUser = updateTenantDto.UserId
            //     };
            //     await tenantRepository.UpdateTenantAsync(updatedTenant);
            //     result = NoContent();
            // }
            // return result;
        }

        [HttpDelete("{tenantId}")]
        public async Task<ActionResult> DeleteTenantAsync(long tenantId)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // Tenant existingTenant = await tenantRepository.GetTenantAsync(tenantId);
            // if(existingTenant is null) {
            //     result = NotFound();
            // } else {
            //     await tenantRepository.DeleteTenantAsync(tenantId);
            //     result = NoContent();
            // }
            // return result;
        }
    }
}