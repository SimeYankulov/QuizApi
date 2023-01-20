using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Shared.Models;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        // GET: api/<TeamsController>
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<TeamModel>>> GetTeams()
        {
            try
            {
                return await _teamService.GetTeams();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database :"+ex.Message);
            }
        }
        // GET api/<TeamsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<TeamModel>> GetTeam(int id)
        {
            try
            {
                var TeamM = await _teamService.GetTeam(id);
                if (TeamM == null) return NotFound();
                return TeamM;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        // POST api/<TeamsController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PostTeam(TeamModel TeamM)
        {
            try
            {
                if (TeamM == null)
                    return BadRequest();

                await _teamService.AddTeam(TeamM);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record:" + ex.Message);
            }
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateTeam(int id, [FromBody] Team info)
        {
            try
            {
                var teamm = await _teamService.GetTeam(id);
                if (teamm == null)
                    return NotFound();

                await _teamService.UpdateTeam(teamm,id);
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
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                var team = await _teamService.GetTeam(id);

                if (team == null)
                {
                    return NotFound();
                }
                await _teamService.DeleteTeam(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}
