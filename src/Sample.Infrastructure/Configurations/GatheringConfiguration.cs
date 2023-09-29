using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Domain.Entities;

namespace Sample.Infrastructure.Configurations;

internal sealed class GatheringConfiguration : IEntityTypeConfiguration<Gathering>
{
    public void Configure(EntityTypeBuilder<Gathering> builder)
    {
        builder.ToTable("Gatherings");

        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => !x.Cancelled);

        builder
            .HasOne(x => x.Member)
            .WithMany();

        builder
            .HasMany(x => x.Invitations())
            .WithOne()
            .HasForeignKey(x => x.GatheringId);

        builder
            .HasMany(x => x.Attendees())
            .WithOne()
            .HasForeignKey(x => x.GatheringId);
    }
}