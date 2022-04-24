using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class InfrastructureContext : IdentityDbContext<AppUser>
    {

        public InfrastructureContext(DbContextOptions<InfrastructureContext> options) : base(options)
        {
        }
    }
}
