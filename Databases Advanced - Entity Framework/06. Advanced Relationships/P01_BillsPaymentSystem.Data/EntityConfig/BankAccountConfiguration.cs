using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.BankAccountId);

            builder.Property(x => x.Balance)
                .IsRequired();

            builder.Property(x => x.BankName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(x => x.SwiftCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();

            builder
                .HasOne(x => x.PaymentMethod)
                .WithOne(x => x.BankAccount)
                .HasForeignKey<PaymentMethod>(x => x.BankAccountId);
        }
    }
}
