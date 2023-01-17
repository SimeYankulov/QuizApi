using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetRoles();
        Task AddRole(RoleModel role);
        Task<String> GetRole(int roleId);
    }
}
