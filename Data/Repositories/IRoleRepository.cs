using Shared.Models;

namespace Data.Repositories
{
    public interface IRoleRepository
    {
        Task<List<RoleModel>> GetRoles();
        Task AddRole(RoleModel role);
        Task<string> GetRole(int roleId);
    }
}
