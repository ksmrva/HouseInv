using ErrorOr;
using HouseInv.Api.Models.Dtos.Tenants;

namespace HouseInv.Api.Services
{
    public interface ITenantsService
    {
        public Task<ErrorOr<TenantDto>> CreateTenantAsync(CreateTenantDto createTenantDto);

        public Task<ErrorOr<TenantDto>> GetTenantAsync(long tenantId);

        public Task<ErrorOr<List<TenantDto>>> GetTenantsAsync();

        public Task<ErrorOr<Updated>> UpdateTenantAsync(long tenantId, UpdateTenantDto updateTenantDto);

        public Task<ErrorOr<Deleted>> DeleteTenantAsync(long tenantId);
    }
}