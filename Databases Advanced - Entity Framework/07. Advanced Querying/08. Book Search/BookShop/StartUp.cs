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

                string givenString = Console.ReadLine().ToLower();

                string titles = GetBookTitlesContaining(db, givenString);

                Console.WriteLine(titles);
            }
        }

        public static string GetBookTitlesContaining(BookShopContext db, string givenString)
        {
            var bookTitles = db.Books
                               .Where(x => x.Title.ToString().ToLower().Contains(givenString.ToLower()))
                               .Select(x => x.Title)
                               .OrderBy(x => x)
                               .ToArray();

            return string.Join(Environment.NewLine, bookTitles);
        }
    }
}
