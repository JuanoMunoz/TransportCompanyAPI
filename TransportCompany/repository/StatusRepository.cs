using Microsoft.EntityFrameworkCore;
using TransportCompany.context;
using TransportCompany.Dto_s.Statuses;
using TransportCompany.Interface;
using TransportCompany.Models;

namespace TransportCompany.repository
{
    public class StatusRepository : IStatus
    {
        private readonly TransportDBContext _context;
        public StatusRepository(TransportDBContext context) {
        _context = context; 
        }

        public async Task CreateStatus(Status status)
        {
            await _context.Statuses.AddAsync(status);
            await _context.SaveChangesAsync();
        }

        public async Task<Status?> DeleteStatusAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null) { return null; }
            _context.Remove(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<List<Status>> GetAllStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status?> GetStatusByIdAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null) { return null; }
            return status;
        }

        public async Task<Status?> UpdateStatusAsync(int id, UpdateStatusDTO updateDto)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null) { return null; }
            status.Name = updateDto.Name;
            await _context.SaveChangesAsync();
            return status;
        }

    }
}
