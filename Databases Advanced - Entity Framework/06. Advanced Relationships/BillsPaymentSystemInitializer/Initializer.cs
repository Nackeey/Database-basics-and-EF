using P01_BillsPaymentSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystemInitializer
{
    public class Initializer
    {
        public void Seed(BillsPaymentSystemContext context)
        {
            InsertUsers(context);
            InsertCreditCards(context);
            InsertBankAccounts(context);
            InsertPaymentMethods(context);
        }

        private void InsertPaymentMethods(BillsPaymentSystemContext context)
        {
            var paymentMethods = PaymentMethodInitializer.GetPaymentMethods();

            for (int i = 0; i < paymentMethods.Length; i++)
            {
                if (IsValid(paymentMethods[i]))
                {
                    context.PaymentMethods.Add(paymentMethods[i]);
                }
            }

            context.SaveChanges();
        }

        private void InsertBankAccounts(BillsPaymentSystemContext context)
        {
            var bankAccounts = BankAccountInitializer.GetBankAccounts();

            for (int i = 0; i < bankAccounts.Length; i++)
            {
                if (IsValid(bankAccounts[i]))
                {
                    context.BankAccounts.Add(bankAccounts[i]);
                }
            }

            context.SaveChanges();
        }

        private void InsertCreditCards(BillsPaymentSystemContext context)
        {
            var creditCards = CreditCardInitializer.GetCreditCards();

            for (int i = 0; i < creditCards.Length; i++)
            {
                if (IsValid(creditCards[i]))
                {
                    context.CreditCards.Add(creditCards[i]);
                }
            }

            context.SaveChanges();
        }

        private void InsertUsers(BillsPaymentSystemContext context)
        {
            var users = UserInitializer.GetUsers();

            for (int i = 0; i < users.Length; i++)
            {
                if (IsValid(users[i]))
                {
                    context.Users.Add(users[i]);
                }
            }

            context.SaveChanges(); 
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}
