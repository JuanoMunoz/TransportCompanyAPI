using TransportCompany.Dto_s.Branches;
using TransportCompany.Models;

namespace TransportCompany.Interface
{
    public interface IBranch
    {
        public Task<List<Branch>> GetBranchAsync();
        public Task<Branch?> GetBranchByIdAsync(int id);

        public Task CreateBranchAsync(Branch branch);
        public Task<Branch?> UpdateBranchAsync(int id, UpdateBranchDTO updateBrancheDTO);
        public Task<Branch?> DeleteBranchAsync(int id);
    }
}
