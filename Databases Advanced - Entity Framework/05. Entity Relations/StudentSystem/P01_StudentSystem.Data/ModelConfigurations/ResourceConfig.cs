using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.ModelConfigurations
{
    public class ResourceConfig : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(x => x.ResourceId);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Url)
                .IsUnicode(false);
        }
    }
}
