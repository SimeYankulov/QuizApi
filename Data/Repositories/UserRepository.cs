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
                cfg.CreateMap<User, UserVM>()
                .ReverseMap();
            });
             mapper = config.CreateMapper();
        }

        public async Task<List<UserVM>> GetUsers()
        {
            return  mapper.Map<List<User>,List<UserVM>>(await Users.ToListAsync());
        }

        public async Task AddUser(UserVM user)
        {
            
            await Users.AddAsync(mapper.Map<UserVM,User>(user));
            await SaveChangesAsync();
        }

       public async Task<UserVM> GetUser(int id)
        {
            return mapper.Map<User,UserVM>(await Users.FindAsync(id));
        }
        //prob
        public async Task DeleteUser(int id)
        {
            var user = await Users.FindAsync(id);
            Users.Remove(user);
            await SaveChangesAsync();
        }
        //prob
        public async Task UpdateUser(UserVM user,int id)
        {
            var userdb = await Users.FindAsync(id);

            userdb.FirstName = user.FirstName;
            userdb.LastName = user.LastName;
            userdb.Email = user.Email;

            Users.Update(userdb);
            //Users.Update(mapper.Map<UserVM,User>(user));
            await SaveChangesAsync();
        }
    }
}
// update user method , viewmodels , validations
