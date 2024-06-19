using Microsoft.EntityFrameworkCore;
using TransportCompany.context;
using TransportCompany.Dto_s.Trucks;
using TransportCompany.Interface;
using TransportCompany.Mapper;
using TransportCompany.Models;

namespace TransportCompany.repository
{
    public class TruckRepository : ITruck
    {
        private readonly TransportDBContext _context;
        public TruckRepository(TransportDBContext context) {
            _context = context;
        }

        public async Task CreateTruckAsync(Truck truck)
        {
            await _context.Trucks.AddAsync(truck);
            await _context.SaveChangesAsync();
        }

        public async Task<Truck?> DeleteTruckAsync(int id)
        {
            var truck = await _context.Trucks.FirstOrDefaultAsync(t=>t.TruckNro.Equals(id));
            if (truck == null) { return null; }
            _context.Trucks.Remove(truck);
            await _context.SaveChangesAsync();
            return truck;
        }

        public async Task<Truck?> GetTruckByIdAsync(int id)
        {
            var truck = await _context.Trucks.Include(t => t.RouteFrom).Include(t => t.RouteTo).Include(t => t.Status).FirstOrDefaultAsync(t => t.TruckNro.Equals(id));
            if (truck == null) { return null; }
            return truck;
        }

        public async  Task<List<Truck>> GetTrucksAsync()
        {
            return await _context.Trucks.Include(t=>t.RouteFrom).Include(t=>t.RouteTo).Include(t=>t.Status).ToListAsync();
        }

        public async Task<Truck?> UpdateStatusTruckAsync(int id, UpdateStatusTruckDTO updateTruckDTO)
        {
            var truck = await _context.Trucks.FirstOrDefaultAsync(t => t.TruckNro.Equals(id));
            if (truck == null) { return null; }
            truck.StatusId = updateTruckDTO.StatusId;
            await _context.SaveChangesAsync();
            return truck;
        }

        public async Task<Truck?> UpdateTruckAsync(int id, UpdateTruckDTO updateTruckDTO)
        {
            var truck = await _context.Trucks.FirstOrDefaultAsync(t => t.TruckNro.Equals(id));
            if (truck == null) { return null; }
            truck.TruckModel = updateTruckDTO.TruckModel;
            truck.InsuranceName = updateTruckDTO.InsuranceName;
            truck.Owner = updateTruckDTO.Owner;
            truck.Mobile = updateTruckDTO.Mobile;
            truck.RouteFromId = updateTruckDTO.RouteFromId;
            truck.RouteToId = updateTruckDTO.RouteToId;

            await _context.SaveChangesAsync();
            return truck;
        }
    }
}
