using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    public static class TaskTrackMapping
    {
        public static void Map(EntityTypeBuilder<TaskTrack> entity)
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(e => e.Tasks)
                 .WithMany(x => x.TasksTrack)
                 .HasForeignKey(e => e.TaskId);
        }
    }
}
