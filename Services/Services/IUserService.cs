using QuizApi.Entities;

namespace QuizApi.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task AddUser(User user);
        Task<User> GetUser(int id);
        Task DeleteUser(User user);
        Task UpdateUser(User user);
    }
}
