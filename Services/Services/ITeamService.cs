using Data.Entities;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
