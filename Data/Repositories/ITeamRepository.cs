
using Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ITeamRepository
    {
        Task AddTeam(Team team);
        Task DeleteTeam(Team team);
        Task<Team> GetTeam(int id);
        Task<List<Team>> GetTeams();
        Task UpdateTeam(Team team);
    }
}
