using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystemInitializer
{
    public class BankAccountInitializer
    {
        public static BankAccount[] GetBankAccounts()
        {
            var bankAccounts = new BankAccount[]
            {
                new BankAccount() { BankName = "Our Bank", SwiftCode = "OBB", Balance = 0.50m },
                new BankAccount() { BankName = "Deuche Bank", SwiftCode = "DB", Balance = 0.75m },
                new BankAccount() { BankName = "Red Bank", SwiftCode = "RBA", Balance = 0.80m },
                new BankAccount() { BankName = "CSKA Bank", SwiftCode = "CBK", Balance = 1.96m },
                new BankAccount() { BankName = "Fake Bank", SwiftCode = "FBI", Balance = 3.55m }
            };

            return bankAccounts;
        }
    }
}
