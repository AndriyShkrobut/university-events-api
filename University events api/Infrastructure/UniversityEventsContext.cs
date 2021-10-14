using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class UniversityEventsContext : DbContext
    {
        public UniversityEventsContext(DbContextOptions<UniversityEventsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        //public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

           // modelBuilder.ApplyConfiguration(new UserConfiguration());         
        }
    }
}

