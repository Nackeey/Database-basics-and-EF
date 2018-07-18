using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.Enums;

namespace BillsPaymentSystemInitializer
{
    public class PaymentMethodInitializer
    {
        public static PaymentMethod[] GetPaymentMethods()
        {
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod() { PaymentType = PaymentType.BankAccount, UserId = 1, BankAccountId = 1, CreditCardId = null },
                new PaymentMethod() { PaymentType = PaymentType.BankAccount, UserId = 2, BankAccountId = 2, CreditCardId = null },
                new PaymentMethod() { PaymentType = PaymentType.BankAccount, UserId = 3, BankAccountId = 3, CreditCardId = null },
                new PaymentMethod() { PaymentType = PaymentType.CreditCard, UserId = 4, BankAccountId = null, CreditCardId = 1 },
                new PaymentMethod() { PaymentType = PaymentType.CreditCard, UserId = 5, BankAccountId = null, CreditCardId = 2 }
            };

            return paymentMethods;
        }
    }
}
