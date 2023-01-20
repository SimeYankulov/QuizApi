using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System.Security.Cryptography;

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
                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: user.Password!,
                            salt: salt,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 10000,
                            numBytesRequested: 256 / 8));

                await Users.AddAsync(mapper.Map<UserModel, User>(user));
                await SaveChangesAsync();

                var tempUser = await Users.SingleOrDefaultAsync(
                    a => a.Email == user.Email && a.Password == user.Password);
                tempUser!.Password = hashed;
                tempUser!.Salt = salt;

                Users.Update(tempUser);

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
                return mapper.Map<User?, UserModel>(await Users.FindAsync(id));
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
                Users.Remove(user!);
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

                userdb!.FirstName = user.FirstName;
                userdb.LastName = user.LastName;
                userdb.Email = user.Email;
                userdb.Password = user.Password;
                userdb.RoleId = user.RoleId;

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

        public async Task<int> GetUserRole(UserLogin user)
        {
            try
            {
                var usr = await Users.SingleOrDefaultAsync(a => a.Email == user.Email);
                if (usr != null) { return usr.RoleId; }

                else return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<bool> FindUser(string? email)
        {
            try
            {
               var tempUser = await Users.SingleOrDefaultAsync(
                    a => a.Email == email);
                if (tempUser != null) { return true; }
                else return false; 

            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public async Task<bool> VerifyPassword(UserLogin loginCredentials)
        {
            try
            {
                var tempUser = await Users.SingleOrDefaultAsync(
                     a => a.Email == loginCredentials.Email);
                
                if (tempUser == null) { return false; }

                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: loginCredentials.Password!,
                                salt: tempUser.Salt!,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8));

                if (hashed == tempUser.Password) { return true; } else return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
