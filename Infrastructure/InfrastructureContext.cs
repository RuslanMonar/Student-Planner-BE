using Domain;
using Infrastructure.Data.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class InfrastructureContext : IdentityDbContext<AppUser>, IInfrastructureContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Project> Projects { get; set; }

        public InfrastructureContext(DbContextOptions<InfrastructureContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(GroupMapping.Map);
            modelBuilder.Entity<Project>(ProjectMapping.Map);

            base.OnModelCreating(modelBuilder);
        }
    }
}
