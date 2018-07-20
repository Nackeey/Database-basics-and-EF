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

                string result = GetMostRecentBooks(db);
                Console.WriteLine(result);
            }
        }

        public static string GetMostRecentBooks(BookShopContext db)
        {
            var categories = db.Categories
                               .OrderBy(x => x.Name)
                               .Select(x => new
                               {
                                   Name = x.Name,
                                   Books = x.CategoryBooks.Select(s => new
                                   {
                                       s.Book.Title,
                                       s.Book.ReleaseDate
                                   })
                                   .OrderByDescending(s => s.ReleaseDate)
                                   .Take(3).ToArray()
                               })
                               .ToArray();

            var sb = new StringBuilder();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");

                foreach (var title in category.Books)
                {
                    sb.AppendLine($"{title.Title} ({title.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
