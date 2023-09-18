using Microsoft.EntityFrameworkCore;
using PMQuanLy.Models;

namespace PMQuanLy.Data
{
    public class PMQLDbContext : DbContext
    {
        public PMQLDbContext(DbContextOptions<PMQLDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
