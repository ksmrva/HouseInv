using ErrorOr;
using HouseInv.Api.Errors;
using HouseInv.Api.Models.Dtos.Resources;
using HouseInv.Api.Models.Entities.Resources;
using HouseInv.Api.Repositories;

namespace HouseInv.Api.Services
{
    public class ResourcesService : IResourcesService
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public ResourcesService(HouseInvDbContext houseInvDbContext)
        {
            this.houseInvDbContext = houseInvDbContext;
        }

        public async Task<ErrorOr<ResourceDto>> CreateResourceAsync(CreateResourceDto createResourceDto)
        {
            var utcNowValue = DateTime.UtcNow;
            Resource resource = new()
            {
                HouseId = createResourceDto.HouseId,
                Name = createResourceDto.Name,
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createResourceDto.UserId,
                ModifiedUser = createResourceDto.UserId
            };
            await houseInvDbContext.Resource.AddAsync(resource);
            await houseInvDbContext.SaveChangesAsync();
            return resource.AsDto();
        }

        public async Task<ErrorOr<ResourceDto>> GetResourceAsync(long resourceId)
        {
            ErrorOr<ResourceDto> result;
            Resource resource = await houseInvDbContext.Resource.FindAsync(resourceId);
            if (resource != null)
            {
                result = resource.AsDto();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<List<ResourceDto>>> GetResourcesAsync()
        {
            ErrorOr<List<ResourceDto>> result;
            IEnumerable<Resource> resourcesResult = houseInvDbContext.Resource;
            if (resourcesResult != null && resourcesResult.Any())
            {
                IEnumerable<ResourceDto> resourceDtos = resourcesResult.Select(resource => resource.AsDto());
                result = resourceDtos.ToList();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<Updated>> UpdateResourceAsync(long resourceId, UpdateResourceDto updateResourceDto)
        {
            ErrorOr<Updated> updateResult;
            Resource existingResource = await houseInvDbContext.Resource.FindAsync(resourceId);
            if (existingResource is null)
            {
                updateResult = DataErrors.DataNotFound;
            }
            else
            {
                if (updateResourceDto.HouseId is <= 0)
                {
                    updateResourceDto.HouseId = existingResource.HouseId;
                }
                if (updateResourceDto.Name is null || updateResourceDto.Name.Length == 0)
                {
                    updateResourceDto.Name = existingResource.Name;
                }

                Resource updatedResource = new()
                {
                    Id = existingResource.Id,
                    CreatedDate = existingResource.CreatedDate.ToUniversalTime(),
                    CreatedUser = existingResource.CreatedUser,

                    Name = updateResourceDto.Name,
                    HouseId = (long)updateResourceDto.HouseId,

                    ModifiedDate = DateTime.UtcNow.ToUniversalTime(),
                    ModifiedUser = updateResourceDto.UserId
                };
                houseInvDbContext.Detach(existingResource);
                houseInvDbContext.Resource.Update(updatedResource);
                await houseInvDbContext.SaveChangesAsync();
                updateResult = Result.Updated;
            }
            return updateResult;
        }

        public async Task<ErrorOr<Deleted>> DeleteResourceAsync(long resourceId)
        {
            ErrorOr<Deleted> deleteResult;
            Resource existingResource = await houseInvDbContext.Resource.FindAsync(resourceId);
            if (existingResource is null)
            {
                deleteResult = DataErrors.DataNotFound;
            }
            else
            {
                houseInvDbContext.Resource.Remove(existingResource);
                await houseInvDbContext.SaveChangesAsync();
                deleteResult = Result.Deleted;
            }
            return deleteResult;
        }
    }
}