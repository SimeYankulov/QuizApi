using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Services;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IRoleService _roleService;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, ITeamService teamService,IRoleService roleService, IConfiguration config)
        {
            _userService = userService;
            _teamService = teamService;
            _roleService = roleService;
            _config = config;
        }
        // GET: api/<UsersController>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            try
            {
                return await _userService.GetUsers();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database :" + ex.Message);
            }
        }


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            try
            {
                var UserM = await _userService.GetUser(id);
                if (UserM == null) return NotFound();
                return UserM;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database : " + ex.Message);
            }
        }

        // POST api/<UsersController>
         [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> PostUser(UserModel user)
        {
            try
            {
                if (user == null)
                    return BadRequest();
                if (await _roleService.GetRole(user.RoleId) != null)
                {
                    await _userService.AddUser(user);
                    return Ok();
                }
                else return NotFound("Invalid user role");
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record :" + ex.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserModel info)
        {
            try
            {
                var userm = await _userService.GetUser(id);
                if (userm == null)
                    return NotFound();

                await _userService.UpdateUser(info, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating user record:" + ex.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);

                if (user == null)
                {
                    return NotFound();
                }
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }

        // PUT api/<UsersController>/
        [HttpPut("{userid}/team/{teamid}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddUserToTeam(int userid, int teamid)
        {

            try
            {
                var user = await _userService.GetUser(userid);
                var team = await _teamService.GetTeam(teamid);

                if (user != null && team != null)
                {
                    await _userService.AddUserToTeam(userid, teamid);
                    return Ok();
                }
                else if (user == null && team == null) { throw new Exception("User and Team not found"); }
                else if (user == null) { throw new Exception("User not found"); }
                else if (team == null) { throw new Exception("Team not found"); }
                else return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error adding user to team :" + ex.Message);
            }
        }
        // DELETE api/<UsersController>/
        [HttpDelete("{userid}/team/{teamid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveUserFromTeam(int userid, int teamid)
        {
            try
            {
                var user = await _userService.GetUser(userid);
                var team = await _teamService.GetTeam(teamid);

                if (user != null && team != null)
                {
                    await _userService.RemoveUserFromTeam(userid, teamid);
                    return Ok();
                }
                else if (user == null && team == null) { throw new Exception("User and Team not found"); }
                else if (user == null) { throw new Exception("User not found"); }
                else if (team == null) { throw new Exception("Team not found"); }
                else return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error removing user from team :" + ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginAsync(UserLogin user)
        {
            
            if (await _userService.FindUser(user.Email) == false)
            {
                return NotFound("User with email: " + user.Email + " not found.");
            }
            if(await _userService.VerifyPassword(user) == false)
            {
                return NotFound("Password incorrect");
            }

            var userRoleId = await _userService.GetUserRole(user);

            var userRole = await _roleService.GetRole(userRoleId);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                     new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, userRole)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


