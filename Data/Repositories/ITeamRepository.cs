using Shared.Models;

namespace Data.Repositories
{
    public interface ITeamRepository
    {
        Task AddTeam(TeamModel team);
        Task DeleteTeam(int id);
        Task<TeamModel> GetTeam(int id);
        Task<List<TeamModel>> GetTeams();
        Task UpdateTeam(TeamModel team,int id);
    }
}
