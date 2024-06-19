using TransportCompany.Dto_s.Branches;
using TransportCompany.Dto_s.Statuses;
using TransportCompany.Models;

namespace TransportCompany.Dto_s.Trucks
{
    public class TruckDTO
    {
        public int TruckNro { get; set; }
        public string TruckModel { get; set; }

        public string InsuranceName { get; set; }
        public string Owner { get; set; }
        public string Mobile { get; set; }

        public BranchTruckDTO RouteFrom { get; set; }

        public BranchTruckDTO RouteTo { get; set; }

        public StatusDTO Status { get; set; }
    }
}
