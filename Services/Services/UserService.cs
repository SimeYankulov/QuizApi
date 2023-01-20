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
        public async Task<List<UserModel>> GetUsers()
        {
            try
            {
                return await _userRepository.GetUsers();
            }
            catch(Exception ex)
            {
               throw new Exception(ex.Message.ToString());
            }
        }
        public async Task AddUser(UserModel user)
        {
            try
            {
                await _userRepository.AddUser(user);
            }
            catch
            (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public async Task<UserModel> GetUser(int id)
        {
            try
            {
                return await _userRepository.GetUser(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public async Task DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }
        public async Task UpdateUser(UserModel user,int id)
        {
            try
            {
                await _userRepository.UpdateUser(user, id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public async Task AddUserToTeam(int teamId, int userId)
        {
            try
            {
                await _userRepository.AddUserToTeam(teamId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public async Task RemoveUserFromTeam(int userId, int teamId)
        {
            try
            {
                await _userRepository.RemoveUserFromTeam(teamId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public Task<int> GetUserRole(UserLogin user)
        {
            try
            {
                return _userRepository.GetUserRole(user);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public Task<bool> FindUser(string? email)
        {
            try
            {
                return _userRepository.FindUser(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public Task<bool> VerifyPassword(UserLogin loginCredentials)
        {
            try
            {
                return _userRepository.VerifyPassword(loginCredentials);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
