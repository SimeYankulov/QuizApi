//using Microsoft.AspNetCore.Http.HttpResults;
using Data.Entities;
using Data.Repositories;
using Shared.Models;

namespace Services.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserVM>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task AddUser(UserVM user)
        {
            await _userRepository.AddUser(user);
        }

        public async Task<UserVM> GetUser(int id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task DeleteUser(int id)
        {
           await _userRepository.DeleteUser(id);
        }

        public async Task UpdateUser(UserVM user,int id)
        {
           await _userRepository.UpdateUser(user, id);
        }
    }
}
