using HouseInv.Models.Dtos.Houses;
using HouseInv.Models.Dtos.Persons;
using HouseInv.Models.Dtos.Resources;
using HouseInv.Models.Dtos.Resources.Personal;
using HouseInv.Models.Dtos.Resources.Personal.Appliance;
using HouseInv.Models.Dtos.Tenants;
using HouseInv.Models.Entities.Houses;
using HouseInv.Models.Entities.Notes;
using HouseInv.Models.Entities.Persons;
using HouseInv.Models.Entities.Resources;
using HouseInv.Models.Entities.Resources.Personal;
using HouseInv.Models.Entities.Resources.Personal.Appliance;
using HouseInv.Models.Entities.Tenants;

namespace HouseInv
{
    public static class Extensions
    {
        public static PersonDto AsDto(this Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                CreatedDate = person.CreatedDate,
                ModifiedDate = person.ModifiedDate,
                CreatedUser = person.CreatedUser,
                ModifiedUser = person.ModifiedUser
            };
        }

        public static HouseDto AsDto(this House house)
        {
            return new HouseDto
            {
                Id = house.Id,
                Name = house.Name,
                Address1 = house.Address1,
                Address2 = house.Address2,
                City = house.City,
                State = house.State,
                Zip = house.Zip,
                OwnerId = house.OwnerId,
                CreatedDate = house.CreatedDate,
                ModifiedDate = house.ModifiedDate,
                CreatedUser = house.CreatedUser,
                ModifiedUser = house.ModifiedUser
            };
        }

        public static TenantDto AsDto(this Tenant tenant)
        {
            return new TenantDto
            {
                Id = tenant.Id,
                HouseId = tenant.HouseId,
                PersonId = tenant.PersonId,
                CreatedDate = tenant.CreatedDate,
                ModifiedDate = tenant.ModifiedDate,
                CreatedUser = tenant.CreatedUser,
                ModifiedUser = tenant.ModifiedUser
            };
        }

        public static ApplianceDto AsDto(this Appliance appliance)
        {
            return new ApplianceDto
            {
                Id = appliance.Id,
                PersonalResourceId = appliance.PersonalResourceId,
                Brand = appliance.Brand,
                Price = appliance.Price,
                PurchaseDate = appliance.PurchaseDate,
                InstallationDate = appliance.InstallationDate,
                CreatedDate = appliance.CreatedDate,
                ModifiedDate = appliance.ModifiedDate,
                CreatedUser = appliance.CreatedUser,
                ModifiedUser = appliance.ModifiedUser
            };
        }

        public static ResourceDto AsDto(this Resource resource)
        {
            return new ResourceDto
            {
                Id = resource.Id,
                HouseId = resource.HouseId,
                Name = resource.Name,
                CreatedDate = resource.CreatedDate,
                ModifiedDate = resource.ModifiedDate,
                CreatedUser = resource.CreatedUser,
                ModifiedUser = resource.ModifiedUser
            };
        }

        public static CreateResourceDto CreateResourceDtoFromCreateApplianceDto(this CreateApplianceDto createApplianceDto)
        {
            return new CreateResourceDto
            {
                HouseId = createApplianceDto.HouseId,
                Name = createApplianceDto.Name,
                UserId = createApplianceDto.UserId
            };
        }

        public static PersonalResourceDto AsDto(this PersonalResource personalResource)
        {
            return new PersonalResourceDto
            {
                Id = personalResource.Id,
                TenantId = personalResource.TenantId,
                ResourceId = personalResource.ResourceId,
                CreatedDate = personalResource.CreatedDate,
                ModifiedDate = personalResource.ModifiedDate,
                CreatedUser = personalResource.CreatedUser,
                ModifiedUser = personalResource.ModifiedUser
            };
        }

        public static CreateResourceDto CreateResourceDtoFromCreatePersonalResourceDto(this CreatePersonalResourceDto createPersonalResourceDto)
        {
            return new CreateResourceDto
            {
                Name = createPersonalResourceDto.Name,
                HouseId = createPersonalResourceDto.HouseId,
                UserId = createPersonalResourceDto.UserId
            };
        }

        public static CreatePersonalResourceDto CreatePersonalResourceDtoFromCreateApplianceDto(this CreateApplianceDto createApplianceDto)
        {
            return new CreatePersonalResourceDto
            {
                Name = createApplianceDto.Name,
                HouseId = createApplianceDto.HouseId,
                TenantId = createApplianceDto.TenantId,
                UserId = createApplianceDto.UserId
            };
        }

        public static NoteDto AsDto(this Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                NoteValue = note.NoteValue,
                CreatedDate = note.CreatedDate,
                ModifiedDate = note.ModifiedDate,
                CreatedUser = note.CreatedUser,
                ModifiedUser = note.ModifiedUser
            };
        }
    }
}