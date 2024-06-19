using TransportCompany.Dto_s.Trucks;
using TransportCompany.Models;

namespace TransportCompany.Interface
{
    public interface ITruck
    {
        public Task<List<Truck>> GetTrucksAsync();
        public Task CreateTruckAsync(Truck truck);
        public Task<Truck?> GetTruckByIdAsync(int id);
        public Task<Truck?> DeleteTruckAsync(int id);
        public Task<Truck?> UpdateTruckAsync(int id, UpdateTruckDTO updateTruckDTO);
        public Task<Truck?> UpdateStatusTruckAsync(int id, UpdateStatusTruckDTO updateTruckDTO);

    }
}
