using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    public static class ProjectMapping
    {
        public static void Map(EntityTypeBuilder<Project> entity)
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(ui => ui.Group)
               .WithMany(cul => cul.Projects)
               .HasForeignKey(ui => ui.GroupdId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
