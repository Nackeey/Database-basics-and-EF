namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                string totalProfit = GetTotalProfitByCategory(db);

                Console.WriteLine(totalProfit);
            }
        }

        public static string GetTotalProfitByCategory(BookShopContext db)
        {
            var books = db.Categories
                          .Select(x => new
                          {
                              CategoryName = x.Name,
                              TotalProfit = x.CategoryBooks.Sum(c => c.Book.Copies * c.Book.Price)
                          })
                          .OrderByDescending(x => x.TotalProfit)
                          .ThenBy(x => x.CategoryName)
                          .ToArray();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.CategoryName} ${book.TotalProfit:F2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
