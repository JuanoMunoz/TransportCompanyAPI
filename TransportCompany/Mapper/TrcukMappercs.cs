using TransportCompany.Dto_s.Trucks;
using TransportCompany.Models;

namespace TransportCompany.Mapper
{
    public static class TrcukMappercs
    {
        public static TruckDTO ToTruckDTO(this Truck truck)
        {
            return new TruckDTO
            {
                TruckNro = truck.TruckNro,
                TruckModel = truck.TruckModel,
                Owner = truck.Owner,
                Mobile = truck.Mobile,
                InsuranceName = truck.InsuranceName,
                RouteFrom = truck.RouteFrom.ToBranchTruckDTO(),
                RouteTo = truck.RouteTo.ToBranchTruckDTO(),
                Status = truck.Status.ToStatusDTO(),
            };
        }
        public static Truck ToTruck(this CreateTruckDTO truck)
        {
            return new Truck
            {
                TruckModel = truck.TruckModel,
                Owner = truck.Owner,
                Mobile = truck.Mobile,
                InsuranceName = truck.InsuranceName,
                RouteFromId = truck.RouteFromId,
                RouteToId = truck.RouteToId,
                StatusId = truck.StatusId,
            };
        }
    }
}
