//using Microsoft.AspNetCore.Http.HttpResults;
using Data.Entities;
using Data.Repositories;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public Task<int> CheckUser(UserLogin user)
        {
            try
            {
                return _userRepository.GetUser(user);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
