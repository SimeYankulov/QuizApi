using Shared.Models;

namespace Services.Services
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetRoles();
        Task AddRole(RoleModel role);
        Task<String> GetRole(int roleId);
    }
}
