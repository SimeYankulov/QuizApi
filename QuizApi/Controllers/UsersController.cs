using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Services.Services;
using Shared.Models;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetUsers()
        {
            try
            {
                return await _userService.GetUsers();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserVM>> GetUser(int id)
        {
            try
            {
                var UserVM = await _userService.GetUser(id);
                if (UserVM == null) return NotFound();
                return UserVM;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database : "+ex.Message);
            }
        } 
        
        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> PostUser(UserVM user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                await _userService.AddUser(user);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record");
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] UserVM info)
        {
            try
            {
                var user = await _userService.GetUser(id);
                if (user == null)
                    return NotFound();

                /*
                user.FirstName = info.FirstName;
                user.LastName = info.LastName;
                user.Email = info.Email;
                */
                await _userService.UpdateUser(info,id);
                       return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating user record");
            }
        }
        
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);

                if(user == null)
                {
                    return NotFound();
                }
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
