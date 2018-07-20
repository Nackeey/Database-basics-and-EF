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

                int year = int.Parse(Console.ReadLine());

                string books = GetBooksNotRealeasedIn(db, year);

                Console.WriteLine(books);
            }
        }

        public static string GetBooksNotRealeasedIn(BookShopContext db, int year)
        {
            var sb = new StringBuilder();

            var books = db.Books
                .OrderBy(x => x.BookId)
                .Where(x => !x.ReleaseDate.ToString().Contains(year.ToString()))
                .Select(x => x.Title)
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
