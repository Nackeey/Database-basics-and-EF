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
                DbInitializer.ResetDatabase(db);

                //IncreasePrices(db);

                int deletedBooks = RemoveBooks(db);

                Console.WriteLine(deletedBooks + " books were deleted");
            }
        }

        public static int RemoveBooks(BookShopContext db)
        {
            var books = db.Books.Where(x => x.Copies < 4200).ToArray();

            var deletedBooks = books.Length;

            db.RemoveRange(books);

            db.SaveChanges();

            return deletedBooks;
        }

        public static void IncreasePrices(BookShopContext db)
        {
            var books = db.Books
                          .Where(x => x.ReleaseDate.Value.Year < 2010)
                          .ToArray();

            foreach (var book in books)
            {
                book.Price += 5;
            }
        }
    }
}
