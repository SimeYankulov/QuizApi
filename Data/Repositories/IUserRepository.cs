using Data.Entities;
using Shared.Models;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserVM>> GetUsers();
        Task AddUser(UserVM user);
        Task<UserVM> GetUser(int id);
        Task DeleteUser(int id);
        Task UpdateUser(UserVM user,int id);
    }
}
