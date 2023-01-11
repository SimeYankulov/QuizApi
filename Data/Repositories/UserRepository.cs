using AutoMapper;
using Data.Context;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Data.Repositories
{
    public class UserRepository:QuizContext, IUserRepository
    {
        private IMapper mapper;
        public UserRepository(DbContextOptions<QuizContext> options):base(options)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>()
                .ReverseMap();
            });
             mapper = config.CreateMapper();
        }

        public async Task<List<UserModel>> GetUsers()
        {
            try
            {
                return mapper.Map<List<User>, List<UserModel>>
               (await Users.ToListAsync());
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
                await Users.AddAsync(mapper.Map<UserModel, User>(user));
                await SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

       public async Task<UserModel> GetUser(int id)
        {
            try
            {
                return mapper.Map<User, UserModel>(await Users.FindAsync(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                var user = await Users.FindAsync(id);
                Users.Remove(user);
                await SaveChangesAsync();

            }catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }

        public async Task UpdateUser(UserModel user,int id)
        {
            try
            {
                var userdb = await Users.FindAsync(id);

                userdb.FirstName = user.FirstName;
                userdb.LastName = user.LastName;
                userdb.Email = user.Email;

                Users.Update(userdb);
                await SaveChangesAsync();

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
                var user = await Users.FindAsync(userId);
                var team = await Teams.FindAsync(teamId);

                Team_User temp = new Team_User();

                if(user != null && team != null)
                {
                    temp.UserId = userId;
                    temp.TeamId = teamId;
                  
                    TeamUsers.Add(temp);
                    await SaveChangesAsync();
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            
        }

        public async Task RemoveUserFromTeam(int teamId, int userId)
        {
            try
            {
                var user = await Users.FindAsync(userId);
                var team = await Teams.FindAsync(teamId);

                Team_User temp = new Team_User();

                if (user != null && team != null)
                {
                    temp.UserId = userId;
                    temp.TeamId = teamId;
                    temp.user = user;
                    temp.team = team;

                    try
                    {
                        TeamUsers.Remove(temp);
                        await SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("User is not in the team\n"+ex.Message.ToString());
                    }
                }
                else throw new Exception("User or Team not found!");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }
    }
}
