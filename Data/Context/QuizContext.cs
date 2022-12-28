using Microsoft.EntityFrameworkCore;
using QuizApi.Entities;
namespace QuizApi.Context
{

    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext>options):base(options)
        { 
           
        }
        public DbSet<User> Users { get; set; }
    }
}
