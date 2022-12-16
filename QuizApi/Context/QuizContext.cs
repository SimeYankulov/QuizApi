using Microsoft.EntityFrameworkCore;
using QuizApi.Entities;
namespace QuizApi.Context
{

    public class QuizContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public QuizContext(IConfiguration configuration)
        { 
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("QuizDataBase"));
        }
        public DbSet<User> Users { get; set; }
    }
}
