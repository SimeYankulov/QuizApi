using AutoMapper;
using Data.Context;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TeamRepository : QuizContext, ITeamRepository
    {
        private IMapper mapper;
        public TeamRepository(DbContextOptions<QuizContext> options) : base(options)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Team, TeamModel>()
                .ReverseMap();
            });
            mapper = config.CreateMapper();
        }

        public async Task AddTeam(TeamModel team)
        {
            try
            {
                await Teams.AddAsync(mapper.Map<TeamModel, Team>(team));
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task DeleteTeam(int id)
        {
            try
            {
                var team = await Teams.FindAsync(id);
                Teams.Remove(team);
                await SaveChangesAsync();
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
                return mapper.Map<Team, TeamModel>
                    (await Teams.FindAsync(id));
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
                return mapper.Map<List<Team>, List<TeamModel>>
                 (await Teams.ToListAsync());
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
                var teamdb = await Teams.FindAsync(id);

                teamdb.Name = team.Name;
                teamdb.Captain_Name = team.Captain_Name;
                teamdb.Points = team.Points;

                Teams.Update(teamdb);
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
