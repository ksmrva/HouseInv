using HouseInv.Models.Dtos.Resources.Personal.Appliance;
using HouseInv.Models.Entities.Resources;
using HouseInv.Models.Entities.Resources.Personal;
using HouseInv.Models.Entities.Resources.Personal.Appliance;
using HouseInv.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Controllers
{
    [ApiController]
    [Route("appliances")]
    public class AppliancesController : ControllerBase
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public AppliancesController(HouseInvDbContext houseInvDbContext) 
        {
            this.houseInvDbContext = houseInvDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<ApplianceDto>> CreateApplianceAsync(CreateApplianceDto createApplianceDto)
        {
            var utcNowValue = DateTime.UtcNow;

            Resource resource = new()
            {
                HouseId = createApplianceDto.HouseId,
                Name = createApplianceDto.Name,
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createApplianceDto.UserId,
                ModifiedUser = createApplianceDto.UserId
            };
            houseInvDbContext.Add(resource);
            houseInvDbContext.SaveChanges();

            PersonalResource personalResource = new()
            {
                ResourceId = resource.Id,
                TenantId = createApplianceDto.TenantId,
                CreatedDate = utcNowValue,
                ModifiedDate = utcNowValue,
                CreatedUser = createApplianceDto.UserId,
                ModifiedUser = createApplianceDto.UserId
            };
            houseInvDbContext.Add(personalResource);
            houseInvDbContext.SaveChanges();

            Appliance appliance = new()
            {
                PersonalResourceId = personalResource.Id,
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
            return CreatedAtAction(nameof(GetApplianceAsync), new { applianceId = appliance.Id }, appliance.AsDto() );
        }

        [HttpGet("{applianceId}")]
        public async Task<ActionResult<ApplianceDto>> GetApplianceAsync(long applianceId)
        {
            ActionResult<ApplianceDto> actionResult;
            Appliance applianceResult = await houseInvDbContext.Appliance.FindAsync(applianceId);
            if (applianceResult == null) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(applianceResult.AsDto());
            }
            return actionResult;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplianceDto>>> GetAppliancesAsync()
        {
            ActionResult<IEnumerable<ApplianceDto>> actionResult;
            IEnumerable<Appliance> appliancesResult = houseInvDbContext.Appliance;
            if (appliancesResult == null || !(appliancesResult.Any())) 
            {
                actionResult = NotFound();
            }
            else 
            {
                actionResult = Ok(appliancesResult.Select(appliance => appliance.AsDto()));
            }
            return await Task.FromResult(actionResult);
        }

        [HttpPut("{applianceId}")]
        public async Task<ActionResult> UpdateApplianceAsync(long applianceId, UpdateApplianceDto updateApplianceDto)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // Appliance existingAppliance = await applianceRepository.GetApplianceAsync(applianceId);
            // if(existingAppliance is null) {
            //     result = NotFound();
            // } else {

            //     // if(updateApplianceDto.HouseId is null || updateApplianceDto.HouseId.Length == 0) {
            //     //     updateApplianceDto.HouseId = existingAppliance.HouseId;
            //     // }
            //     // if(updateApplianceDto.TenantId is null || updateApplianceDto.TenantId.Length == 0) {
            //     //     updateApplianceDto.TenantId = existingAppliance.TenantId;
            //     // }
            //     if(updateApplianceDto.Name is null || updateApplianceDto.Name.Length == 0) {
            //         updateApplianceDto.Name = existingAppliance.Name;
            //     }
            //     if(updateApplianceDto.Brand is null || updateApplianceDto.Brand.Length == 0) {
            //         updateApplianceDto.Brand = existingAppliance.Brand;
            //     }
            //     if(updateApplianceDto.Price is null || updateApplianceDto.Price <= 0.00m) {
            //         updateApplianceDto.Price = existingAppliance.Price;
            //     }
                
            //     Appliance updatedAppliance = existingAppliance with {
            //         Name = updateApplianceDto.Name,
            //         Brand = updateApplianceDto.Brand,
            //         Price = (decimal)updateApplianceDto.Price,
            //         ModifiedDate = DateTimeOffset.UtcNow,
            //         ModifiedUser = updateApplianceDto.UserId
            //     };
            //     await applianceRepository.UpdateApplianceAsync(updatedAppliance);
            //     result = NoContent();
            // }
            // return result;
        }

        [HttpDelete("{applianceId}")]
        public async Task<ActionResult> DeleteApplianceAsync(long applianceId)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // Appliance existingAppliance = await applianceRepository.GetApplianceAsync(applianceId);
            // if(existingAppliance is null) {
            //     result = NotFound();
            // } else {
            //     await applianceRepository.DeleteApplianceAsync(applianceId);
            //     result = NoContent();
            // }
            // return result;
        }
    }
}