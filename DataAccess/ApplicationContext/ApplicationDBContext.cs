using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataAccess.ApplicationContext
{
    public class ApplicationDBContext
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext() : base()
            {

            }

            public ApplicationDbContext(DbContextOptions options) : base(options)
            {

            }
            public DbSet<Users> users { get; set; }
        }

    }
}
