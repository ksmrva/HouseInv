using ErrorOr;
using HouseInv.Errors;
using HouseInv.Models.Dtos.Tenants;
using HouseInv.Models.Entities.Tenants;
using HouseInv.Repositories;

namespace HouseInv.Services
{
    public class TenantsService : ITenantsService
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public TenantsService(HouseInvDbContext houseInvDbContext)
        {
            this.houseInvDbContext = houseInvDbContext;
        }

        public async Task<ErrorOr<TenantDto>> CreateTenantAsync(CreateTenantDto createTenantDto)
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
            return tenant.AsDto();
        }

        public async Task<ErrorOr<TenantDto>> GetTenantAsync(long tenantId)
        {
            ErrorOr<TenantDto> result;
            Tenant tenant = await houseInvDbContext.Tenant.FindAsync(tenantId);
            if (tenant != null)
            {
                result = tenant.AsDto();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<List<TenantDto>>> GetTenantsAsync()
        {
            ErrorOr<List<TenantDto>> result;
            IEnumerable<Tenant> tenantsResult = houseInvDbContext.Tenant;
            if (tenantsResult != null && tenantsResult.Any())
            {
                IEnumerable<TenantDto> tenantDtos = tenantsResult.Select(tenant => tenant.AsDto());
                result = tenantDtos.ToList();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<Updated>> UpdateTenantAsync(long tenantId, UpdateTenantDto updateTenantDto)
        {
            ErrorOr<Updated> updateResult;
            Tenant existingTenant = await houseInvDbContext.Tenant.FindAsync(tenantId);
            if (existingTenant is null)
            {
                updateResult = DataErrors.DataNotFound;
            }
            else
            {
                if (updateTenantDto.HouseId <= 0)
                {
                    updateTenantDto.HouseId = existingTenant.HouseId;
                }
                if (updateTenantDto.PersonId <= 0)
                {
                    updateTenantDto.PersonId = existingTenant.PersonId;
                }

                Tenant updatedTenant = existingTenant with
                {
                    HouseId = (long)updateTenantDto.HouseId,
                    PersonId = (long)updateTenantDto.PersonId,
                    // Adding the CreatedDate here to get around the DateTime needing to be converted to UTC
                    CreatedDate = existingTenant.CreatedDate.ToUniversalTime(),
                    ModifiedDate = DateTime.UtcNow.ToUniversalTime(),
                    ModifiedUser = updateTenantDto.UserId
                };
                houseInvDbContext.Detach(existingTenant);
                houseInvDbContext.Tenant.Update(updatedTenant);
                await houseInvDbContext.SaveChangesAsync();
                updateResult = Result.Updated;
            }
            return updateResult;
        }

        public async Task<ErrorOr<Deleted>> DeleteTenantAsync(long tenantId)
        {
            ErrorOr<Deleted> deleteResult;
            Tenant existingTenant = await houseInvDbContext.Tenant.FindAsync(tenantId);
            if (existingTenant is null)
            {
                deleteResult = DataErrors.DataNotFound;
            }
            else
            {
                houseInvDbContext.Tenant.Remove(existingTenant);
                await houseInvDbContext.SaveChangesAsync();
                deleteResult = Result.Deleted;
            }
            return deleteResult;
        }
    }
}