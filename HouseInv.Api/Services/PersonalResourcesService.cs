using System.Data;
using ErrorOr;
using HouseInv.Api.Errors;
using HouseInv.Api.Models.Dtos.Resources;
using HouseInv.Api.Models.Dtos.Resources.Personal;
using HouseInv.Api.Models.Dtos.Resources.Personal.Appliance;
using HouseInv.Api.Models.Entities.Resources.Personal;
using HouseInv.Api.Repositories;

namespace HouseInv.Api.Services
{
    public class PersonalResourcesService : IPersonalResourcesService
    {
        private readonly HouseInvDbContext houseInvDbContext;
        private readonly IResourcesService resourcesService;

        public PersonalResourcesService(HouseInvDbContext houseInvDbContext, IResourcesService resourcesService)
        {
            this.houseInvDbContext = houseInvDbContext;
            this.resourcesService = resourcesService;
        }

        public async Task<ErrorOr<PersonalResourceDto>> CreatePersonalResourceAsync(CreatePersonalResourceDto createPersonalResourceDto)
        {
            var utcNowValue = DateTime.UtcNow;

            ResourceDto createdResourceDto = null;
            CreateResourceDto createResourceDto = createPersonalResourceDto.CreateResourceDtoFromCreatePersonalResourceDto();
            ErrorOr<ResourceDto> errorOrResourceDto = await resourcesService.CreateResourceAsync(createResourceDto);

            errorOrResourceDto.Match(
                resourceDto => createdResourceDto = resourceDto,
                errors => throw new DataException()
            );

            PersonalResource personalResource = new()
            {
                TenantId = createPersonalResourceDto.TenantId,
                ResourceId = createdResourceDto.Id,
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createPersonalResourceDto.UserId,
                ModifiedUser = createPersonalResourceDto.UserId
            };
            await houseInvDbContext.PersonalResource.AddAsync(personalResource);
            await houseInvDbContext.SaveChangesAsync();
            return personalResource.AsDto();
        }

        public async Task<ErrorOr<PersonalResourceDto>> GetPersonalResourceAsync(long personalResourceId)
        {
            ErrorOr<PersonalResourceDto> result;
            PersonalResource personalResource = await houseInvDbContext.PersonalResource.FindAsync(personalResourceId);
            if (personalResource != null)
            {
                result = personalResource.AsDto();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<List<PersonalResourceDto>>> GetPersonalResourcesAsync()
        {
            ErrorOr<List<PersonalResourceDto>> result;
            IEnumerable<PersonalResource> personalResourcesResult = houseInvDbContext.PersonalResource;
            if (personalResourcesResult != null && personalResourcesResult.Any())
            {
                IEnumerable<PersonalResourceDto> personalResourceDtos = personalResourcesResult.Select(personalResource => personalResource.AsDto());
                result = personalResourceDtos.ToList();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<Updated>> UpdatePersonalResourceAsync(long personalResourceId, UpdatePersonalResourceDto updatePersonalResourceDto)
        {
            ErrorOr<Updated> updateResult;
            PersonalResource existingPersonalResource = await houseInvDbContext.PersonalResource.FindAsync(personalResourceId);
            if (existingPersonalResource is null)
            {
                updateResult = DataErrors.DataNotFound;
            }
            else
            {
                if (updatePersonalResourceDto.TenantId <= 0)
                {
                    updatePersonalResourceDto.TenantId = existingPersonalResource.TenantId;
                }
                if (updatePersonalResourceDto.ResourceId <= 0)
                {
                    updatePersonalResourceDto.ResourceId = existingPersonalResource.ResourceId;
                }

                PersonalResource updatedPersonalResource = new()
                {
                    Id = existingPersonalResource.Id,
                    CreatedDate = existingPersonalResource.CreatedDate.ToUniversalTime(),
                    CreatedUser = existingPersonalResource.CreatedUser,

                    TenantId = (long)updatePersonalResourceDto.TenantId,
                    ResourceId = (long)updatePersonalResourceDto.ResourceId,

                    ModifiedDate = DateTime.UtcNow.ToUniversalTime(),
                    ModifiedUser = updatePersonalResourceDto.UserId
                };
                houseInvDbContext.Detach(existingPersonalResource);
                houseInvDbContext.PersonalResource.Update(updatedPersonalResource);
                await houseInvDbContext.SaveChangesAsync();
                updateResult = Result.Updated;
            }
            return updateResult;
        }

        public async Task<ErrorOr<Deleted>> DeletePersonalResourceAsync(long personalResourceId)
        {
            ErrorOr<Deleted> deleteResult;
            PersonalResource existingPersonalResource = await houseInvDbContext.PersonalResource.FindAsync(personalResourceId);
            if (existingPersonalResource is null)
            {
                deleteResult = DataErrors.DataNotFound;
            }
            else
            {
                // Store off the Resource ID so that we can delete the Resource record after deleting the Personal Resource record
                long resourceId = existingPersonalResource.ResourceId;

                // Delete the Personal Resource record
                houseInvDbContext.PersonalResource.Remove(existingPersonalResource);
                await houseInvDbContext.SaveChangesAsync();

                // Delete the Resource record
                ErrorOr<Deleted> deleteResourceResult = await resourcesService.DeleteResourceAsync(resourceId);
                deleteResourceResult.Match(
                    deleted => deleted,
                    errors => throw new DataException()
                );

                deleteResult = Result.Deleted;
            }

            return deleteResult;
        }
    }
}