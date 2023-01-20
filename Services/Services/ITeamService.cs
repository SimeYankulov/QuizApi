using Shared.Models;

namespace Services.Services
{
    public interface ITeamService
    {
        Task<List<TeamModel>> GetTeams();
        Task AddTeam(TeamModel team);
        Task<TeamModel> GetTeam(int id);
        Task DeleteTeam(int id);
        Task UpdateTeam(TeamModel team,int id);
    }
}
