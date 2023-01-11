using Data.Entities;
using Shared.Models;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetUsers();
        Task AddUser(UserModel user);
        Task<UserModel> GetUser(int id);
        Task DeleteUser(int id);
        Task UpdateUser(UserModel user,int id);

        Task AddUserToTeam(int teamId,int userId);
        Task RemoveUserFromTeam(int teamId, int userId);
    }
}
