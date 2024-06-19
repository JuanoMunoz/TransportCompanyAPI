using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportCompany.Models;
using TransportCompany.context;
using TransportCompany.Interface;
using TransportCompany.Mapper;
using TransportCompany.Dto_s.Trucks;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using TransportCompany.Helpers;

namespace TransportCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly ITruck _truckRepo;
        private readonly IBranch _branchRepo;
        private readonly IStatus _statusRepo;

        public TrucksController(ITruck truckRepo, IBranch branchRepo, IStatus statusRepo)
        {
            _truckRepo = truckRepo;
            _branchRepo = branchRepo;
            _statusRepo = statusRepo;
        }

        // GET: api/Trucks
        [HttpGet]
        public async Task<IActionResult> GetTrucks()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var trucks = await _truckRepo.GetTrucksAsync();
            var finalTrucks = trucks.Select(x=>x.ToTruckDTO()).ToList();
            return Ok(finalTrucks);
        }

        // GET: api/Trucks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTruck(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var truck = await _truckRepo.GetTruckByIdAsync(id);

            if (truck == null)
            {
                return NotFound();
            }

            return Ok(truck.ToTruckDTO());
        }

        [HttpPut("status/{id}")]
        [Authorize(Roles = "admin,gerente")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateStatusTruckDTO updateStatusTruck)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var status = await _statusRepo.GetStatusByIdAsync(updateStatusTruck.StatusId);
            if (status == null )
            {
                var json = JsonSerializer.Serialize(new ApiErrorMessage("THE DELIVERED STATUS DOES NOT MATCH  ANY OF THE RECORDS FROM THE DATABASE", "YOU CAN VIEW THE STATUSES IN THE DEDICATED ENDPOINT"));
                return BadRequest(json);
            }
            var truck = await _truckRepo.UpdateStatusTruckAsync(id, updateStatusTruck);
            if (truck == null) { return NotFound(); }
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> PutTruck(int id, UpdateTruckDTO truckDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var branchFrom = await _branchRepo.GetBranchByIdAsync(truckDto.RouteFromId);
            var branchTo = await _branchRepo.GetBranchByIdAsync(truckDto.RouteToId);
            if (branchFrom == null || branchTo == null)
            {
                var json = JsonSerializer.Serialize(new ApiErrorMessage("ONE OF THE BRANCHES YOU ARE TRYING TO LINK DOES NOT EXISTS", "YOU CAN WATCH THE BRANCHES IN THE DEDICATED ENDPOINT"));
                return BadRequest(json);
            }

            var truck = await _truckRepo.UpdateTruckAsync(id, truckDto);
            if(truck == null) { return NotFound(); }
            return NoContent();
        }

        // POST: api/Trucks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Truck>> PostTruck(CreateTruckDTO createTruckDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var truck = createTruckDTO.ToTruck();
            var branchFrom = await _branchRepo.GetBranchByIdAsync(createTruckDTO.RouteFromId);
            var branchTo = await _branchRepo.GetBranchByIdAsync(createTruckDTO.RouteToId);
            if (branchFrom == null || branchTo == null) {
                var json = JsonSerializer.Serialize(new ApiErrorMessage("ONE OF THE BRANCHS YOU ARE TRYING TO LINK DOES NOT EXISTS", "YOU CAN WATCH THE BRANCHES IN THE DEDICATED ENDPOINT"));
                return BadRequest(json); 
            }            
            var status = await _statusRepo.GetStatusByIdAsync(createTruckDTO.StatusId);
            if (status == null) {
                var json = JsonSerializer.Serialize(new ApiErrorMessage("THE DELIVERED STATUS DOES NOT MATCH  ANY OF THE RECORDS FROM THE DATABASE", "YOU CAN VIEW THE STATUSES IN THE DEDICATED ENDPOINT"));
                return BadRequest(json); 
            }

            await _truckRepo.CreateTruckAsync(truck);
            return CreatedAtAction("GetTruck", new { id = truck.TruckNro }, truck.ToTruckDTO());
        }

        // DELETE: api/Trucks/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var truck = await _truckRepo.DeleteTruckAsync(id);
            if (truck == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
