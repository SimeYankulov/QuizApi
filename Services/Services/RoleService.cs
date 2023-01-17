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
    public class RoleService : IRoleService
    {
        public readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository= roleRepository;
        }
        public async Task AddRole(RoleModel role)
        {
            try
            {
                await _roleRepository.AddRole(role);
            }
            catch
            (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<string> GetRole(int roleId)
        {
            try
            {
                return await _roleRepository.GetRole(roleId);
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
                return await _roleRepository.GetRoles();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
