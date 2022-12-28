using QuizApi.Entities;

namespace QuizApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task AddUser(User user);
        Task<User> GetUser(int id);
        Task DeleteUser(User user);
        Task UpdateUser(User user);
    }
}
