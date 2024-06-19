using Microsoft.EntityFrameworkCore;
using TransportCompany.context;
using TransportCompany.Dto_s.Branches;
using TransportCompany.Interface;
using TransportCompany.Models;

namespace TransportCompany.repository
{
    public class BranchRepository : IBranch
    {
        private readonly TransportDBContext _context;
        public BranchRepository(TransportDBContext context) {
            _context = context;
        }
        public async Task CreateBranchAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
        }

        public async Task<Branch?> DeleteBranchAsync(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if(branch == null) { return null; }
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<List<Branch>> GetBranchAsync()
        {
            return await _context.Branches.Include(b=>b.Users).ToListAsync();
        }

        public async Task<Branch?> GetBranchByIdAsync(int id)
        {
            var Branch = await _context.Branches.Include(b=>b.Users).FirstOrDefaultAsync(x=>x.Id == id);
            if (Branch == null)
            {
                return null;
            }
            return Branch;
        }

        public async Task<Branch?> UpdateBranchAsync(int id, UpdateBranchDTO updateBrancheDTO)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) { return null;}
            branch.Telephone = updateBrancheDTO.Telephone;
            branch.Code = updateBrancheDTO.Code;
            branch.Name = updateBrancheDTO.Name;
            branch.City = updateBrancheDTO.City;
            await _context.SaveChangesAsync();
            return branch;
        }
    }
}
