
using Data.Entities;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
