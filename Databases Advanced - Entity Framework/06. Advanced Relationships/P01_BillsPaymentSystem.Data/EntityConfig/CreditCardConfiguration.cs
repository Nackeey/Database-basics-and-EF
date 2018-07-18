using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(x => x.CreditCardId);

            builder.Property(x => x.Limit)
                .IsRequired();

            builder.Property(x => x.MoneyOwed)
                .IsRequired();

            builder.Property(x => x.ExpirationDate)
                .IsRequired();

            builder.Ignore(x => x.LimitLeft);

            builder
                .HasOne(x => x.PaymentMethod)
                .WithOne(x => x.CreditCard)
                .HasForeignKey<PaymentMethod>(x => x.CreditCardId);
        }
    }
}
