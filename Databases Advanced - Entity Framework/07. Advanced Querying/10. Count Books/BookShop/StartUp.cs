namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
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

                int lengthCheck = int.Parse(Console.ReadLine());

                int booksCount = CountBooks(db, lengthCheck);

                Console.WriteLine(booksCount);
            }
        }

        public static int CountBooks(BookShopContext db, int lengthCheck)
        {
            var books = db.Books
                          .Where(x => x.Title.Length > lengthCheck)
                          .ToArray();

            return books.Count();
        }
    }
}
