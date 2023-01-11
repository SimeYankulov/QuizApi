using Data.Entities;
using Data.Repositories;
using Shared.Models;
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

        public async Task AddTeam(TeamModel team)
        {
            try
            {
                await _teamRepository.AddTeam(team);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
       
        }

        public async Task DeleteTeam(int id)
        {
            try
            {
                await _teamRepository.DeleteTeam(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<TeamModel> GetTeam(int id)
        {
            try
            {
                return await _teamRepository.GetTeam(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<List<TeamModel>> GetTeams()
        {
            try
            {
                return await _teamRepository.GetTeams();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public async Task UpdateTeam(TeamModel team,int id)
        {
            try
            {
                await _teamRepository.UpdateTeam(team, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
