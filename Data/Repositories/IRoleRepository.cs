using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRoleRepository
    {
        Task<List<RoleModel>> GetRoles();
        Task AddRole(RoleModel role);
        Task<string> GetRole(int roleId);
    }
}
