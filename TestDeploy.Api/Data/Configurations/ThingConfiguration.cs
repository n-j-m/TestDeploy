using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestDeploy.Api.Entities;

namespace TestDeploy.Api.Data.Configurations
{
    public class ThingConfiguration : IEntityTypeConfiguration<Thing>
    {
        public void Configure(EntityTypeBuilder<Thing> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired();
        }
    }
}