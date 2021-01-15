using ChaosInitiative.Web.ControlPanel.Model;
using Microsoft.EntityFrameworkCore;

namespace ChaosInitiative.Web.ControlPanel.Services
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
        
        public DbSet<Game> Games { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().HasKey(i => new { i.GameId, i.IssueId });
            
            modelBuilder.Entity<Game>().Property(g => g.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Release>().HasKey(r => new {r.GameId, r.VersionId});

            modelBuilder.Entity<ReleaseFeatures>().HasKey(rf => new {rf.ReleaseId, rf.FeatureId});
        }

    }
    
}