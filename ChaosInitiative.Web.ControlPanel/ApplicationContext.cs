using ChaosInitiative.Web.ControlPanel.Model;
using Microsoft.EntityFrameworkCore;

namespace ChaosInitiative.Web.ControlPanel
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
        
        public DbSet<Game> Games { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Feature> Features { get; set;  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(g => g.Id).ValueGeneratedOnAdd();
        }

    }
    
}