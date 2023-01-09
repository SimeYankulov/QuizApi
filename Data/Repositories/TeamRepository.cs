using Data.Context;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TeamRepository : QuizContext, ITeamRepository
    {
        public TeamRepository(DbContextOptions<QuizContext> options) : base(options)
        {
        }

        public async Task AddTeam(Team team)
        {
           await Teams.AddAsync(team);
            await SaveChangesAsync();
        }

        public async Task DeleteTeam(Team team)
        {
            Teams.Remove(team);
            await SaveChangesAsync();
        }

        public async Task<Team> GetTeam(int id)
        {
            return await Teams.FindAsync(id);
        }

        public async Task<List<Team>> GetTeams()
        {
            return await Teams.ToListAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            Teams.Update(team);
            await SaveChangesAsync();
        }
    }
}
