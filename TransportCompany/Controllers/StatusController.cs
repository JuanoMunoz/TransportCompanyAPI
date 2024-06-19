using Microsoft.AspNetCore.Mvc;
using TransportCompany.Models;
using Microsoft.AspNetCore.Authorization;
using TransportCompany.Interface;
using TransportCompany.Dto_s;
using TransportCompany.Mapper;
using TransportCompany.Dto_s.Statuses;

namespace TransportCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatus _statusRepo;

        public StatusController(IStatus status)
        {
            _statusRepo = status;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            var statuses =await _statusRepo.GetAllStatusesAsync();
            var statusesDTO = statuses.Select(s => s.ToStatusDTO());
            return Ok(statusesDTO);
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            var status = await _statusRepo.GetStatusByIdAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status.ToStatusDTO());
        }

        // PUT: api/Status/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutStatus(int id, UpdateStatusDTO status)
        {
            var tryinUpdateStatus = await _statusRepo.UpdateStatusAsync(id,status);
            if(tryinUpdateStatus == null) { return NotFound(); }

            return NoContent();
        }

        // POST: api/Status
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Status>> PostStatus(CreateStatusDTO status)
        {
            var NewStatus = status.ToStatus();
            await _statusRepo.CreateStatus(NewStatus);
            return CreatedAtAction("GetStatus", new { id = NewStatus.Id }, status);
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _statusRepo.DeleteStatusAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
