using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RoleRepository : QuizContext, IRoleRepository
    {
        private IMapper mapper;
        public RoleRepository(DbContextOptions<QuizContext> options) : base(options)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, RoleModel>()
                .ReverseMap();
            });
            mapper = config.CreateMapper();
        }

        public async Task AddRole(RoleModel role)
        {
            try
            {
                await Roles.AddAsync(mapper.Map<RoleModel, Role>(role));
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<string> GetRole(int roleId)
        {
            try
            {   
                var role = await Roles.FindAsync(roleId);
                return role.RoleName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<List<RoleModel>> GetRoles()
        {
            try
            {
                return mapper.Map<List<Role>, List<RoleModel>>
               (await Roles.ToListAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
