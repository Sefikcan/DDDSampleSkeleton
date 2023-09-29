using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Domain.Entities;
using Sample.Domain.ValueObjects.Member;

namespace Sample.Infrastructure.Configurations;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, value => Email.Create(value).Value());

        builder.Property(x => x.FirstName)
            .HasConversion(x => x.Value, value => FirstName.Create(value).Value())
            .HasMaxLength(FirstName.MaxLength);
        
        builder.Property(x => x.LastName)
            .HasConversion(x => x.Value, value => LastName.Create(value).Value())
            .HasMaxLength(LastName.MaxLength);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}