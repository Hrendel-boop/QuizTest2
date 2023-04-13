using Microsoft.EntityFrameworkCore;
using QuizTest.Models;

namespace QuizTest
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Test> Test { get; set; }
    }
}
