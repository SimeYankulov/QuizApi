//using Microsoft.AspNetCore.Http.HttpResults;
using QuizApi.Entities;
using QuizApi.Repositories;

namespace QuizApi.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task AddUser(User user)
        {
            await _userRepository.AddUser(user);
        }

        public async Task<User> GetUser(int id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task DeleteUser(User user)
        {
           await _userRepository.DeleteUser(user);
        }

        public async Task UpdateUser(User user)
        {
           await _userRepository.UpdateUser(user);
        }
    }
}
