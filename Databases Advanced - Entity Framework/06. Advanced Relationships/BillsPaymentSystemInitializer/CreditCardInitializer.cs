using P01_BillsPaymentSystem.Data.Models;
using System;

namespace BillsPaymentSystemInitializer
{
    public class CreditCardInitializer
    {
        public static CreditCard[] GetCreditCards()
        {
            var creditCards = new CreditCard[]
            {
                new CreditCard() { Limit = 10000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-12)},
                new CreditCard() { Limit = 1000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-5)},
                new CreditCard() { Limit = 5000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-10)},
                new CreditCard() { Limit = 2500, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-16)},
                new CreditCard() { Limit = 3800, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-2)},
            };

            return creditCards;
        }
    }
}
