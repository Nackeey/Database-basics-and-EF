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

                var givenRestrinction = Console.ReadLine().ToLower();

                string titles = GetBooksByAgeRestriction(db, givenRestrinction);

                Console.WriteLine(titles);
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext db, string command)
        {
            var bookTitles = new List<string>();
            AgeRestriction ageRestriction;

            if (Enum.TryParse(command, true, out ageRestriction))
            {
                var books = db.Books.Where(x => x.AgeRestriction == ageRestriction).Select(x => x.Title).ToList();

                foreach (var title in books)
                {
                    bookTitles.Add(title);
                }
            }

            var sb = new StringBuilder();

            foreach (var title in bookTitles.OrderBy(x => x))
            {
                sb.AppendLine(title);
            };

            return sb.ToString();
        }
    }
}
