using System.Data;
using ErrorOr;
using HouseInv.Errors;
using HouseInv.Models.Dtos.Resources.Personal;
using HouseInv.Models.Dtos.Resources.Personal.Appliance;
using HouseInv.Models.Entities.Resources.Personal.Appliance;
using HouseInv.Repositories;

namespace HouseInv.Services
{
    public class AppliancesService : IAppliancesService
    {
        private readonly HouseInvDbContext houseInvDbContext;

        private readonly IResourcesService resourcesService;

        private readonly IPersonalResourcesService personalResourcesService;

        public AppliancesService(HouseInvDbContext houseInvDbContext, IResourcesService resourcesService, IPersonalResourcesService personalResourcesService)
        {
            this.houseInvDbContext = houseInvDbContext;
            this.resourcesService = resourcesService;
            this.personalResourcesService = personalResourcesService;
        }

        public async Task<ErrorOr<ApplianceDto>> CreateApplianceAsync(CreateApplianceDto createApplianceDto)
        {
            var utcNowValue = DateTime.UtcNow;

            PersonalResourceDto createdPersonalResourceDto = null;
            CreatePersonalResourceDto createPersonalResourceDto = createApplianceDto.CreatePersonalResourceDtoFromCreateApplianceDto();
            ErrorOr<PersonalResourceDto> errorOrPersonalResourceDto = await personalResourcesService.CreatePersonalResourceAsync(createPersonalResourceDto);

            errorOrPersonalResourceDto.Match(
                personalResourceDto => createdPersonalResourceDto = personalResourceDto,
                errors => throw new DataException()
            );

            Appliance appliance = new()
            {
                PersonalResourceId = createdPersonalResourceDto.Id,
                Brand = createApplianceDto.Brand,
                Price = createApplianceDto.Price,
                PurchaseDate = createApplianceDto.PurchaseDate.ToUniversalTime(),
                InstallationDate = createApplianceDto.InstallationDate.ToUniversalTime(),
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createApplianceDto.UserId,
                ModifiedUser = createApplianceDto.UserId
            };
            await houseInvDbContext.Appliance.AddAsync(appliance);
            await houseInvDbContext.SaveChangesAsync();
            return appliance.AsDto();
        }

        public async Task<ErrorOr<ApplianceDto>> GetApplianceAsync(long applianceId)
        {
            ErrorOr<ApplianceDto> result;
            Appliance appliance = await houseInvDbContext.Appliance.FindAsync(applianceId);
            if (appliance != null)
            {
                result = appliance.AsDto();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<List<ApplianceDto>>> GetAppliancesAsync()
        {
            ErrorOr<List<ApplianceDto>> result;
            IEnumerable<Appliance> appliancesResult = houseInvDbContext.Appliance;
            if (appliancesResult != null && appliancesResult.Any())
            {
                IEnumerable<ApplianceDto> applianceDtos = appliancesResult.Select(appliance => appliance.AsDto());
                result = applianceDtos.ToList();
            }
            else
            {
                result = DataErrors.DataNotFound;
            }
            return await Task.FromResult(result);
        }

        public async Task<ErrorOr<Updated>> UpdateApplianceAsync(long applianceId, UpdateApplianceDto updateApplianceDto)
        {
            ErrorOr<Updated> updateResult;
            Appliance existingAppliance = await houseInvDbContext.Appliance.FindAsync(applianceId);
            if (existingAppliance is null)
            {
                updateResult = DataErrors.DataNotFound;
            }
            else
            {
                if (updateApplianceDto.Brand is null || updateApplianceDto.Brand.Length == 0)
                {
                    updateApplianceDto.Brand = existingAppliance.Brand;
                }
                if (updateApplianceDto.Price is null || updateApplianceDto.Price <= 0)
                {
                    updateApplianceDto.Price = existingAppliance.Price;
                }
                if (updateApplianceDto.PurchaseDate is null)
                {
                    updateApplianceDto.PurchaseDate = existingAppliance.PurchaseDate;
                }
                if (updateApplianceDto.InstallationDate is null)
                {
                    updateApplianceDto.InstallationDate = existingAppliance.InstallationDate;
                }
                Appliance updatedAppliance = new()
                {
                    Id = existingAppliance.Id,
                    PersonalResourceId = existingAppliance.PersonalResourceId,
                    CreatedDate = existingAppliance.CreatedDate.ToUniversalTime(),
                    CreatedUser = existingAppliance.CreatedUser,

                    Brand = updateApplianceDto.Brand,
                    Price = (decimal)updateApplianceDto.Price,
                    PurchaseDate = ((DateTime)updateApplianceDto.PurchaseDate).ToUniversalTime(),
                    InstallationDate = ((DateTime)updateApplianceDto.InstallationDate).ToUniversalTime(),

                    ModifiedDate = DateTime.UtcNow.ToUniversalTime(),
                    ModifiedUser = updateApplianceDto.UserId
                };
                houseInvDbContext.Detach(existingAppliance);
                houseInvDbContext.Appliance.Update(updatedAppliance);
                await houseInvDbContext.SaveChangesAsync();
                updateResult = Result.Updated;
            }
            return updateResult;
        }

        public async Task<ErrorOr<Deleted>> DeleteApplianceAsync(long applianceId)
        {
            ErrorOr<Deleted> deleteResult;
            Appliance existingAppliance = await houseInvDbContext.Appliance.FindAsync(applianceId);
            if (existingAppliance is null)
            {
                deleteResult = DataErrors.DataNotFound;
            }
            else
            {
                // Delete the Appliance record
                houseInvDbContext.Appliance.Remove(existingAppliance);
                await houseInvDbContext.SaveChangesAsync();

                // Delete the Personal Resource record
                ErrorOr<Deleted> deletePersonalResourceResult = await personalResourcesService.DeletePersonalResourceAsync(existingAppliance.PersonalResourceId);
                deletePersonalResourceResult.Match(
                    deleted => deleted,
                    errors => throw new DataException()
                );

                deleteResult = Result.Deleted;
            }

            return deleteResult;
        }
    }
}