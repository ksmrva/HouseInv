using ErrorOr;
using HouseInv.Models.Dtos.Resources;

namespace HouseInv.Services
{
    public interface IResourcesService
    {
        public Task<ErrorOr<ResourceDto>> CreateResourceAsync(CreateResourceDto createResourceDto);

        public Task<ErrorOr<ResourceDto>> GetResourceAsync(long resourceId);

        public Task<ErrorOr<List<ResourceDto>>> GetResourcesAsync();

        public Task<ErrorOr<Updated>> UpdateResourceAsync(long resourceId, UpdateResourceDto updateResourceDto);

        public Task<ErrorOr<Deleted>> DeleteResourceAsync(long resourceId);
    }
}