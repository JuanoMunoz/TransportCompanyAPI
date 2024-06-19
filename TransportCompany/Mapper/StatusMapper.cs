using TransportCompany.Dto_s;
using TransportCompany.Dto_s.Statuses;
using TransportCompany.Models;

namespace TransportCompany.Mapper
{
    public static class StatusMapper
    {
        public static Status ToStatus(this CreateStatusDTO createStatusDTO)
        {
            return new Status
            {
                Name = createStatusDTO.Name,
            };
        }
        public static StatusDTO ToStatusDTO(this Status Status)
        {
            return new StatusDTO
            {
                Id = Status.Id, 
                Name = Status.Name
            };
        }
    }
}
