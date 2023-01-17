using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Shared.Models;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController :ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/<RolesController>
        [HttpGet]
       // [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetRoles()
        {
            try
            {
                return await _roleService.GetRoles();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database :" + ex.Message);
            }
        }
        // POST api/<RolesController>
        [HttpPost]
    //    [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddRole(RoleModel role)
        {
            try
            {
                if (role == null)
                    return BadRequest();

                await _roleService.AddRole(role);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new role record:" + ex.Message);
            }
        }

    }
}
