using HouseInv.Models.Dtos.Tenants;
using HouseInv.Models.Entities.Houses;
using HouseInv.Models.Entities.Persons;
using HouseInv.Models.Entities.Resources;
using HouseInv.Models.Entities.Resources.Personal;
using HouseInv.Models.Entities.Resources.Personal.Appliance;
using HouseInv.Models.Entities.Tenants;

namespace HouseInv.Repositories.InMem
{
    public class InMemResourcesRepository : IAppliancesRepository
    {
        private const string UserId = "kmark";

        private const long PersonId = 1;

        private const long HouseId = 1;

        private const long TenantId = 1;

        private readonly Person person = new()
        {
            Id = PersonId,
            FirstName = "Kevin",
            LastName = "Mark",
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            CreatedUser = UserId,
            ModifiedUser = UserId
        };
        private readonly House house = new()
        {
            Id = HouseId,
            Name = "Primary",
            Address1 = "7929 Wistar Woods Court",
            City = "Richmond",
            State = "VA",
            Zip = "23228",
            OwnerId = PersonId,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            CreatedUser = UserId,
            ModifiedUser = UserId
        };

        private readonly TenantDto tenant = new()
        {
            Id = TenantId,
            PersonId = PersonId,
            HouseId = HouseId,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            CreatedUser = UserId,
            ModifiedUser = UserId
        };

        private readonly List<Appliance> appliances;

        public InMemResourcesRepository()
        {
            this.appliances = [];

            // Created Microwave
            Resource microwaveResource = new()
            {
                Id = 1,
                Name = "Microwave",
                HouseId = house.Id,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedUser = UserId,
                ModifiedUser = UserId
            };
            PersonalResource microwavePersonalResource = new()
            {
                Id = 1,
                TenantId = TenantId,
                ResourceId = microwaveResource.Id,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedUser = UserId,
                ModifiedUser = UserId
            };
            Appliance microwave = new Appliance
            {
                Id = 1,
                PersonalResourceId = 1,
                Brand = "GE",
                Price = 352.20M,
                PurchaseDate = DateTime.Parse("05/10/2019"),
                InstallationDate = DateTime.Parse("05/15/2019"),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedUser = UserId,
                ModifiedUser = UserId
            };
            this.appliances.Add(microwave);

            // Create Refrigerator
            Resource refrigeratorResource = new()
            {
                Id = 2,
                Name = "Refrigerator",
                HouseId = house.Id,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedUser = UserId,
                ModifiedUser = UserId
            };
            PersonalResource refrigeratorPersonalResource = new()
            {
                Id = 2,
                TenantId = TenantId,
                ResourceId = 1,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedUser = UserId,
                ModifiedUser = UserId
            };
            Appliance refrigerator = new Appliance
            {
                Id = 2,
                PersonalResourceId = 2,
                Brand = "GE",
                Price = 1295.50M,
                PurchaseDate = DateTime.Parse("05/12/2019"),
                InstallationDate = DateTime.Parse("05/15/2019"),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedUser = UserId,
                ModifiedUser = UserId
            };
            this.appliances.Add(refrigerator);
        }

        public void CreateAppliance(Appliance appliance)
        {
            throw new NotImplementedException();
            // appliances.Add(appliance);
        }

        public Appliance GetAppliance(long applianceId)
        {
            return appliances.Where(appliance => appliance.Id == applianceId).SingleOrDefault();
        }

        public IEnumerable<Appliance> GetAppliances()
        {
            return appliances;
        }

        public void UpdateAppliance(Appliance updatedAppliance)
        {
            throw new NotImplementedException();
            // var indexOfAppliance = appliances.FindIndex(existingAppliance => existingAppliance.Id == updatedAppliance.Id);
            // if(indexOfAppliance == -1) {
            //    // NotFound();
            // }
            // appliances[indexOfAppliance] = updatedAppliance;
        }

        public void DeleteAppliance(long applianceId)
        {
            throw new NotImplementedException();
            // var indexOfAppliance = appliances.FindIndex(existingAppliance => existingAppliance.Id == applianceId);
            // if(indexOfAppliance == -1) {
            //    // NotFound();
            // }
            // appliances.RemoveAt(indexOfAppliance);
        }
    }
}