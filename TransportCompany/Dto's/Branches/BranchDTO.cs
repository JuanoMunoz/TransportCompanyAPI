
using TransportCompany.Dto_s.Users;
using TransportCompany.Models;

namespace TransportCompany.Dto_s.Branches
{
    public class BranchDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;

        public List<UserDTO> Users { get; set; } = [];
    }
}
