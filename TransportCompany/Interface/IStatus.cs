using TransportCompany.Dto_s.Statuses;
using TransportCompany.Models;

namespace TransportCompany.Interface
{
    public interface IStatus
    {
        public Task<List<Status>> GetAllStatusesAsync();
        public Task CreateStatus(Status status);
        public Task<Status?> GetStatusByIdAsync(int id);

        public Task<Status?> UpdateStatusAsync(int id, UpdateStatusDTO updateDto);
        public Task<Status?> DeleteStatusAsync(int id);
    }
}
