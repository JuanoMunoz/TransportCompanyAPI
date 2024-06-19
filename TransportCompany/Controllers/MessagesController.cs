using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportCompany.Models;
using TransportCompany.context;
using Microsoft.AspNetCore.Authorization;
using TransportCompany.Interface;
using TransportCompany.Dto_s.Messages;
using TransportCompany.Mapper;
using System.Security.Claims;
using System.Text.Json;
using TransportCompany.Helpers;
using Microsoft.AspNetCore.Identity;

namespace TransportCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessage _messageRepo;
        private readonly UserManager<User> _userManager;

        public MessagesController(IMessage messageRepo, UserManager<User> userManager)
        {
            _messageRepo = messageRepo;
            _userManager = userManager;
        }

        
        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetMessage()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var messages = await _messageRepo.GetAllMessagesAsync();
            var finalMessages = messages.Select(m => m.ToMessageDTO());
            return Ok(finalMessages);

        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetMessage(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var message = await _messageRepo.GetMessageByIdAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message.ToMessageDTO());
        }

        [HttpPost]
        [Authorize(Roles = "visitor,gerente")]
        public async Task<ActionResult<Message>> PostMessage(CreateMessageDTO message)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var userName = User.FindFirstValue(ClaimTypes.Name);
            if (userName == null)
            {
                var jsonError = JsonSerializer.Serialize(new ApiErrorMessage("You are not authorizated to see this", "Try log in"));
                return Unauthorized(jsonError);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            var finalMessage = message.toMessage(user.Id);

            await _messageRepo.CreateMessageAsync(finalMessage);

            return CreatedAtAction("GetMessage", new { id = finalMessage.Id }, finalMessage.ToMessageDTO());
        }

    }
}
