using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public interface IInfrastructureContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Project> Projects { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
