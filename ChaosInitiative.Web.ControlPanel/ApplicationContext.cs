using ChaosInitiative.Web.ControlPanel.Model;
using Microsoft.EntityFrameworkCore;

namespace ChaosInitiative.Web.ControlPanel
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Game>().HasKey(k => k.Name);
            
            modelBuilder.Entity<Release>().ToTable("Release");
            modelBuilder.Entity<Release>().HasKey(k => k.Game);
            modelBuilder.Entity<Release>().HasKey(k => k.VersionId);
            
            modelBuilder.Entity<Feature>().ToTable("Feature");
            modelBuilder.Entity<Feature>().HasKey(k => k.FeatureId);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Feature> Features { get; set; }
    }
    
}