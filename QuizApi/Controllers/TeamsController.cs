using Data.Entities;
using Microsoft.AspNetCore.Mvc;

using Services.Services;

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
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            try
            {
                return await _teamService.GetTeams();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        // GET api/<TeamsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            try
            {
                var team = await _teamService.GetTeam(id);
                if (team == null) return NotFound();
                return team;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST api/<TeamsController>
        [HttpPost]
        public async Task<ActionResult> PostTeam(Team team)
        {
            try
            {
                if (team == null)
                    return BadRequest();

                await _teamService.AddTeam(team);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record");
            }
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Team info)
        {
            try
            {
                var team = await _teamService.GetTeam(id);
                if (team == null)
                    return NotFound();

                //
                team.Name = info.Name;
                team.Captain_Name = info.Captain_Name;
                team.Points = info.Points;
                //
                await _teamService.UpdateTeam(team);
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
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                var team = await _teamService.GetTeam(id);

                if (team == null)
                {
                    return NotFound();
                }
                await _teamService.DeleteTeam(team);
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
