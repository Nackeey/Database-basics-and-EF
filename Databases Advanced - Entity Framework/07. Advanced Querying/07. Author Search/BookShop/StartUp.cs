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

                string input = Console.ReadLine();

                string authors = GetAuthorNamesEndingIn(db, input);

                Console.WriteLine(authors);
            }
        }

        public static string GetAuthorNamesEndingIn(BookShopContext db, string input)
        {
            var authors = db.Authors
                            .OrderBy(x => x)
                            .Select(x => new
                            {
                                FirstName = x.FirstName,
                                LastName = x.LastName
                            })
                            .Where(x => x.FirstName.EndsWith(input))
                            .ToArray();

            var sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine(author.FirstName + ' ' + author.LastName);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
