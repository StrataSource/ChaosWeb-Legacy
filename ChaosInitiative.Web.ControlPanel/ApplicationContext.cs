using ChaosInitiative.Web.ControlPanel.Model;
using Microsoft.EntityFrameworkCore;

namespace ChaosInitiative.Web.ControlPanel
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
        
        public DbSet<Game> Games { get; }
        public DbSet<Release> Releases { get; }
        public DbSet<Feature> Features { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
    
}