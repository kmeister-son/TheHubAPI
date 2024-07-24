using Microsoft.EntityFrameworkCore;
using TheHubAPI.Models;

namespace TheHubAPI.Data
{
    public class TheHubDbContext : DbContext
    {
        public TheHubDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
