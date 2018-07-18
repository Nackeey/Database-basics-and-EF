using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsRequired();


        }
    }
}
