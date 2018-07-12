using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.ModelConfigurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.CourseId);

            builder.Property(x => x.Name)
                .HasMaxLength(80)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Description)
                .IsUnicode()
                .IsRequired(false);

            builder
                .HasMany(x => x.HomeworkSubmissions)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);

            builder
                .HasMany(x => x.Resources)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);

            builder
                .HasMany(x => x.StudentsEnrolled)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);
        }
    }
}
