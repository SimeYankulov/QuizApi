using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task AddTeam(Team team)
        {
            await _teamRepository.AddTeam(team);
        }

        public async Task DeleteTeam(Team team)
        {
            await _teamRepository.DeleteTeam(team);
        }

        public async Task<Team> GetTeam(int id)
        {
            return await _teamRepository.GetTeam(id);
        }

        public async Task<List<Team>> GetTeams()
        {
            return await _teamRepository.GetTeams();
        }

        public async Task UpdateTeam(Team team)
        {
            await _teamRepository.UpdateTeam(team);
        }
    }
}
