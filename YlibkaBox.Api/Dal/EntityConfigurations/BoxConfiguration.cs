using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YlibkaBox.Api.Domain.Entities;

namespace YlibkaBox.Api.Dal.EntityConfigurations;

public class BoxConfiguration : IEntityTypeConfiguration<Box>
{
    public void Configure(EntityTypeBuilder<Box> builder)
    {
        builder.HasKey(x => x.Id)
            .HasName("PK_Box");

        builder.Property(x => x.Specification)
            .IsRequired()
            .HasMaxLength(200);
    }
}