using System;
using System.Linq;
using BillsPaymentSystemInitializer;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            {
                User user = GetUser(context);
                GetInfo(user);
            }
        }

        private static void GetInfo(User user)
        {
            Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            Console.WriteLine($"Bank Accounts:");

            var bankAccounts = user.PaymentMethods.Where(x => x.BankAccount != null).Select(x => x.BankAccount).ToArray();

            foreach (var bankAccount in bankAccounts)
            {
                Console.WriteLine($"-- ID: {bankAccount.BankAccountId}");
                Console.WriteLine($"--- Balance: {bankAccount.BankAccountId:F2}");
                Console.WriteLine($"--- Bank: {bankAccount.BankName}");
                Console.WriteLine($"--- SWIFT: {bankAccount.SwiftCode}");
            }

            Console.WriteLine($"Credit Cards:");

            var creditCards = user.PaymentMethods.Where(x => x.CreditCard != null).Select(x => x.CreditCard).ToArray();

            foreach (var creditCard in creditCards)
            {
                Console.WriteLine($"-- ID: {creditCard.CreditCardId}");
                Console.WriteLine($"--- Limit: {creditCard.Limit:F2}");
                Console.WriteLine($"--- Money Owed: {creditCard.MoneyOwed:F2}");
                Console.WriteLine($"--- Limit Left: {creditCard.LimitLeft:F2}");
                Console.WriteLine($"--- Expiration Date: {creditCard.ExpirationDate.ToString("yyyy/MM")}");
            }
        }

        private static User GetUser(BillsPaymentSystemContext context)
        {
            int userId = int.Parse(Console.ReadLine());

            User user = null;

            while (true)
            {
                user = context.Users
                    .Where(x => x.UserId == userId)
                    .Include(x => x.PaymentMethods)
                    .ThenInclude(x => x.BankAccount)
                    .Include(x => x.PaymentMethods)
                    .ThenInclude(x => x.CreditCard)
                    .FirstOrDefault();

                if (user == null)
                {
                    Console.WriteLine($"User with id {userId} not found!");

                    userId = int.Parse(Console.ReadLine());
                    continue;
                }

                return user;
            }
        }
    }
}
