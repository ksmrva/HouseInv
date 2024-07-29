using ErrorOr;
using HouseInv.Api.Models.Dtos.Resources.Personal.Appliance;

namespace HouseInv.Api.Services
{
    public interface IAppliancesService
    {
        public Task<ErrorOr<ApplianceDto>> CreateApplianceAsync(CreateApplianceDto createApplianceDto);

        public Task<ErrorOr<ApplianceDto>> GetApplianceAsync(long applianceId);

        public Task<ErrorOr<List<ApplianceDto>>> GetAppliancesAsync();

        public Task<ErrorOr<Updated>> UpdateApplianceAsync(long applianceId, UpdateApplianceDto updateApplianceDto);

        public Task<ErrorOr<Deleted>> DeleteApplianceAsync(long applianceId);
    }
}