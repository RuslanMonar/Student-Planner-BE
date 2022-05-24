using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    public static class TasksMapping
    {
        public static void Map(EntityTypeBuilder<Tasks> entity)
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(e => e.Project)
                 .WithMany(x => x.Tasks)
                 .HasForeignKey(e => e.ProjectId);

            
        }
    }
}

