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

                string givenString = Console.ReadLine().ToLower();

                string books = GetBooksByAuthor(db, givenString);

                Console.WriteLine(books);
            }
        }

        public static string GetBooksByAuthor(BookShopContext db, string givenString)
        {
            var books = db.Books
                          .Where(x => x.Author.LastName.ToString().ToLower().StartsWith(givenString.ToLower()))
                          .OrderBy(x => x.BookId)
                          .Select(x => new
                          {
                              Title = x.Title,
                              FirstName = x.Author.FirstName,
                              LastName = x.Author.LastName
                          })
                          .ToArray();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
