using HouseInv.Models.Entities.Resources.Personal.Appliance;

namespace HouseInv.Repositories.InMem
{
    public interface IAppliancesRepository
    {
        void CreateAppliance(Appliance appliance);
        Appliance GetAppliance(long applianceId);
        IEnumerable<Appliance> GetAppliances();
        void UpdateAppliance(Appliance appliance);
        void DeleteAppliance(long applianceId);
    }
}