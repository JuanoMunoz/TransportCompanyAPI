
using TransportCompany.Dto_s.Branches;
using TransportCompany.Models;

namespace TransportCompany.Mapper
{
    public static class BranchMapper
    {
        public static Branch ToBranch(this CreateBranchDTO createBranchDTO)
        {
            return new Branch
            {
                City = createBranchDTO.City,
                Code = createBranchDTO.Code,
                Name = createBranchDTO.Name,
                Telephone = createBranchDTO.Telephone,
            };
        }

        public static BranchDTO ToBranchDTO(this Branch branch)
        {
            return new BranchDTO
            {
                Id = branch.Id,
                City = branch.City,
                Name = branch.Name,
                Code = branch.Code,
                Telephone = branch.Telephone,
                Users = branch.Users.Select(u=>u.ToUserDTO()).ToList(),
            };
        }

        public static BranchTruckDTO ToBranchTruckDTO(this Branch branch)
        {
            return new BranchTruckDTO
            {
                Id = branch.Id,
                City = branch.City,
                Name = branch.Name,
                Code = branch.Code,
                Telephone = branch.Telephone,
            };
        }

    }

}
