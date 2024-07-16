using HouseInv.Models.Dtos.Resources;
using HouseInv.Models.Entities.Resources;
using HouseInv.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Controllers
{
    [ApiController]
    [Route("resources")]
    public class ResourcesController : ControllerBase
    {
        private readonly HouseInvDbContext houseInvDbContext;

        public ResourcesController(HouseInvDbContext houseInvDbContext) 
        {
            this.houseInvDbContext = houseInvDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDto>> CreateResourceAsync(CreateResourceDto createResourceDto)
        {
            throw new NotImplementedException();
            // var utcNowValue = DateTime.UtcNow;
            // Resource resource = new()
            // {
            //     Id = 1,
            //     HouseId = createResourceDto.HouseId,
            //     Name = createResourceDto.Name,
            //     CreatedDate = utcNowValue,
            //     ModifiedDate = utcNowValue,
            //     CreatedUser = createResourceDto.UserId,
            //     ModifiedUser = createResourceDto.UserId
            // };
            // await houseInvDbContext.Resource.AddAsync(resource);
            // await houseInvDbContext.SaveChangesAsync();
            // return CreatedAtAction(nameof(GetResourceAsync), new { id = resource.Id }, resource.AsDto() );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetResourceAsync(long resourceId)
        {
            throw new NotImplementedException();
            // ActionResult<ResourceDto> actionResult;
            // Resource resourceResult = await houseInvDbContext.Resource.FindAsync(resourceId);
            // if (resourceResult == null) 
            // {
            //     actionResult = NotFound();
            // }
            // else 
            // {
            //     actionResult = Ok(resourceResult.AsDto());
            // }
            // return actionResult;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesAsync()
        {
            throw new NotImplementedException();
            // ActionResult<IEnumerable<ResourceDto>> actionResult;
            // IEnumerable<Resource> resourcesResult = houseInvDbContext.Resource;
            // if (resourcesResult == null || !(resourcesResult.Any())) 
            // {
            //     actionResult = NotFound();
            // }
            // else 
            // {
            //     actionResult = Ok(resourcesResult.Select(resource => resource.AsDto()));
            // }
            // return await Task.FromResult(actionResult);
        }

        [HttpPut("{resourceId}")]
        public async Task<ActionResult> UpdateResourceAsync(long resourceId, UpdateResourceDto updateResourceDto)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // Resource existingResource = await resourceRepository.GetResourceAsync(resourceId);
            // if(existingResource is null) {
            //     result = NotFound();
            // } else {
                
            //     // if(updateResourceDto.HouseId is null || updateResourceDto.HouseId.Length == 0) {
            //     //     updateResourceDto.HouseId = existingResource.HouseId;
            //     // }

            //     if(updateResourceDto.Name is null || updateResourceDto.Name.Length == 0) {
            //         updateResourceDto.Name = existingResource.Name;
            //     }
                
            //     Resource updatedResource = existingResource with {
            //         Name = updateResourceDto.Name,
            //         ModifiedDate = DateTime.UtcNow,
            //         ModifiedUser = updateResourceDto.UserId
            //     };
            //     await resourceRepository.UpdateResourceAsync(updatedResource);
            //     result = NoContent();
            // }
            // return result;
        }

        [HttpDelete("{resourceId}")]
        public async Task<ActionResult> DeleteResourceAsync(long resourceId)
        {
            throw new NotImplementedException();
            // ActionResult result;
            // Resource existingResource = await resourceRepository.GetResourceAsync(resourceId);
            // if(existingResource is null) {
            //     result = NotFound();
            // } else {
            //     await resourceRepository.DeleteResourceAsync(resourceId);
            //     result = NoContent();
            // }
            // return result;
        }
    }
}