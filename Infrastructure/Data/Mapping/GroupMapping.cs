using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping
{
    public static class GroupMapping
    {
        public static void Map(EntityTypeBuilder<Group> entity)
        {
            entity.HasKey(e => e.Id);
        }
    }
}
