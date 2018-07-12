using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.ModelConfigurations
{
    public class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(x => new { x.CourseId, x.StudentId });

            builder
                .HasOne(x => x.Student)
                .WithMany(x => x.CourseEnrollments)
                .HasForeignKey(x => x.StudentId);

            builder
                .HasOne(x => x.Course)
                .WithMany(x => x.StudentsEnrolled)
                .HasForeignKey(x => x.CourseId);
        }
    }
}
