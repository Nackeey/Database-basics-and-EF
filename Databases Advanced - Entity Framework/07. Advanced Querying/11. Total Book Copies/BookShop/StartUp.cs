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

                string totalCopies = CountCopiesByAuthor(db);

                Console.WriteLine(totalCopies);
            }
        }

        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var authors = db.Authors
                            .Select(x => new
                            {
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                TotalCopies = x.Books.Sum(c => c.Copies)
                            })
                            .OrderByDescending(x => x.TotalCopies)
                             .ToArray();

            var sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FirstName} {author.LastName} - {author.TotalCopies}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
