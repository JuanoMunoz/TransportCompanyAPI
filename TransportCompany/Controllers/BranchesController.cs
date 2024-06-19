using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportCompany.Models;
using TransportCompany.Interface;
using TransportCompany.Dto_s.Branches;
using TransportCompany.Mapper;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
namespace TransportCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranch _branchRepo;

        public BranchesController(IBranch branchRepo)
        {
            _branchRepo = branchRepo;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult> GetBranches()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var Branches = await _branchRepo.GetBranchAsync();
            var BranchesDTO = Branches.Select(b=>b.ToBranchDTO()).ToList();
            return Ok(BranchesDTO);    
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBranch(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var branch = await _branchRepo.GetBranchByIdAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch.ToBranchDTO());
        }
        // PUT: api/Branches/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutBranch(int id, UpdateBranchDTO branchDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var branch = await _branchRepo.UpdateBranchAsync(id, branchDTO);
            if (branch == null)
            {
                return NotFound();
            }
            return NoContent();

        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Branch>> PostBranch([FromBody]CreateBranchDTO branchDto)
        {
            if (!ModelState.IsValid) {  return BadRequest(ModelState); }
            var branch = branchDto.ToBranch();
            await _branchRepo.CreateBranchAsync(branch);

            return CreatedAtAction("GetBranch", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(summary: "Elimina una sucursal - solo para admin")]
        [SwaggerResponse(401,"You don't have permissions to see this")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var branch = await _branchRepo.DeleteBranchAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
