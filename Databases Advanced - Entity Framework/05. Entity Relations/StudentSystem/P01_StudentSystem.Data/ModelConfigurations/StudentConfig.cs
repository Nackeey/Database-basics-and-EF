﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.ModelConfigurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.StudentId);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired(false);

            builder
                .HasMany(x => x.HomeworkSubmissions)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);

            builder
                .HasMany(x => x.CourseEnrollments)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);
        }
    }
}
