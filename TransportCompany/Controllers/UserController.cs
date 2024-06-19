using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TransportCompany.Dto_s.Trucks;
using TransportCompany.Dto_s.Users;
using TransportCompany.Helpers;
using TransportCompany.Interface;
using TransportCompany.Models;

namespace TransportCompany.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IBranch _branchRepo;
        public UserController(UserManager<User> userManager, IUserService userService, IBranch branch)
        {
            _userManager = userManager;
            _userService = userService;
            _branchRepo = branch;
        }

        [HttpPost("registerGerente")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register(CreateGerenteDTO userDTO)
        {
            var branch = await _branchRepo.GetBranchByIdAsync(userDTO.BranchId);
            if (branch == null )
            {
                var json = JsonSerializer.Serialize(new ApiErrorMessage("THE BRANCH YOU ARE TRYING TO LINK DOES NOT EXIST", "YOU CAN WATCH THE BRANCHES IN THE DEDICATED ENDPOINT"));
                return BadRequest(json);
            }
            var user = new User
            {
                UserName = userDTO.Username,
                BirthDate = userDTO.BirthDate,
                JoiningDate = userDTO.JoiningDate,
                Salary = userDTO.Salary,
                BranchId = userDTO.BranchId,
                Email = userDTO.Email,

            };
            var CreateUser = await _userManager.CreateAsync(user, userDTO.Password);
            if (!CreateUser.Succeeded) { return StatusCode(500, CreateUser.Errors); }
            var addUserToRole = await _userManager.AddToRoleAsync(user, "gerente");
            if (!addUserToRole.Succeeded) { return StatusCode(500, CreateUser.Errors); }
            return StatusCode(201, new NewUserDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await _userService.CreateTokenJWT(user)
            });

        }

        [HttpPost]
        public async Task<IActionResult> registerUser(VisitorDTO visitor)
        {
            var user = new User
            {
                UserName = visitor.Username,
                Email = visitor.Email,
                BranchId = null
            };
            var CreateUser = await _userManager.CreateAsync(user, visitor.Password);
            if (!CreateUser.Succeeded)
            {
                return StatusCode(500, CreateUser.Errors);
            }
            var AddUserToRole = await _userManager.AddToRoleAsync(user, "visitor");
            if (!AddUserToRole.Succeeded) { return StatusCode(500, AddUserToRole.Errors); }
            return StatusCode(201, new NewVisitorDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await _userService.CreateTokenJWT(user)
            });
        }
       
        [HttpGet("Gerentes")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllGerentes()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            var gerentes = new List<User>();

            foreach (var user in usuarios)
            {
                if (await _userManager.IsInRoleAsync(user, "gerente")) {
                    gerentes.Add(user);

                }
            }
            return Ok(gerentes);
        }


        [HttpPost("login")]

        public async Task<IActionResult> Login(Login userTryingLogin)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName == userTryingLogin.UserName.ToLower());
            if (user == null)
            {

                var json = JsonSerializer.Serialize(new ApiErrorMessage(
                    $"The user {userTryingLogin.UserName} does not exists in our database",
                   "Try checking how you write the username or register in our app. (Hola Yoimer)"
               )) ;
                return Unauthorized(json);
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, userTryingLogin.Password);
            if (!checkPassword)
            {
                var errorMsg = JsonSerializer.Serialize(new ApiErrorMessage("Password and/or username incorrect.", "Please check for any typos."));
                return Unauthorized(errorMsg);
            }
            return Ok(new NewUserDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await _userService.CreateTokenJWT(user)
            });

        }
    }
}
