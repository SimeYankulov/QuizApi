using Microsoft.EntityFrameworkCore;
using QuizApi.Context;
using QuizApi.Entities;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace QuizApi.Repositories
{
    public class UserRepository:QuizContext, IUserRepository
    {
        public UserRepository(DbContextOptions<QuizContext> options):base(options)
        {

        }

        public async Task<List<User>> GetUsers()
        {
            return await Users.ToListAsync();
        }

        public async Task AddUser(User user)
        {
            await Users.AddAsync(user);
            await SaveChangesAsync();
        }

       public async Task<User> GetUser(int id)
        {
            return await Users.FindAsync(id);
        }

        public async Task DeleteUser(User user)
        {
            Users.Remove(user);
            await SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            //throw new NotImplementedException();
            // return  await AsyncUpdate();
            Users.Update(user);
            await SaveChangesAsync();
        }
    }
}
