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
                var date = Console.ReadLine();

                string books = GetBooksReleasedBefore(db, date);

                Console.WriteLine(books);
            }
        }

        public static string GetBooksReleasedBefore(BookShopContext db, string date)
        {
            string dateFormat = "dd-MM-yyyy";

            DateTime releaseDate = DateTime.ParseExact(date, dateFormat, CultureInfo.InvariantCulture);

            var books = db.Books
                           .OrderByDescending(x => x.ReleaseDate)
                           .Where(x => x.ReleaseDate.Value < releaseDate)
                           .Select(x => new
                                        {
                                            Title = x.Title,
                                            EditionType = x.EditionType,
                                            Price = x.Price
                                        })
                                          .ToArray();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
