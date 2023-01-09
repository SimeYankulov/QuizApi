using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeams();
        Task AddTeam(Team team);
        Task<Team> GetTeam(int id);
        Task DeleteTeam(Team team);
        Task UpdateTeam(Team team);
    }
}
