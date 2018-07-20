namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                string booksByPrice = GetBooksByPrice(db);

                Console.WriteLine(booksByPrice);
            }
        }

        public static string GetBooksByPrice(BookShopContext db)
        {
            var books = db.Books
                .OrderByDescending(x => x.Price)
                .Where(x => x.Price > 40)
                .Select(x => new
                   {
                       Title = x.Title,
                       Price = x.Price
                   }).ToArray();


            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
