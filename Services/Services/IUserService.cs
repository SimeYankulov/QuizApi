using Shared.Models;

namespace Services.Services
{
    public interface IUserService
    {
        Task<List<UserModel>> GetUsers();
        Task AddUser(UserModel user);
        Task<UserModel> GetUser(int id);
        Task DeleteUser(int id);
        Task UpdateUser(UserModel user,int id);
        Task AddUserToTeam(int teamId,int userId);
        Task RemoveUserFromTeam(int userid, int teamid);
        Task<int> GetUserRole(UserLogin user);
        Task <bool> FindUser(string? email);
        Task <bool> VerifyPassword(UserLogin loginCredentials);
    }
}
