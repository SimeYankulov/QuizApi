using Data.Entities;
using Shared.Models;

namespace Services.Services
{
    public interface IUserService
    {
        Task<List<UserVM>> GetUsers();
        Task AddUser(UserVM user);
        Task<UserVM> GetUser(int id);
        Task DeleteUser(int id);
        Task UpdateUser(UserVM user,int id);
    }
}
