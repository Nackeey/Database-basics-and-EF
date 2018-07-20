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
                string input = Console.ReadLine();

                string titleOfBooks = GetBooksByCategory(db, input);

                Console.WriteLine(titleOfBooks);
            }
        }

        public static string GetBooksByCategory(BookShopContext db, string input)
        {
            string[] categories = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var books = db.Books
                          .Where(x => x.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                          .Select(x => x.Title)
                          .OrderBy(x => x)
                          .ToArray();

            return string.Join(Environment.NewLine, books);
        }
    }
}
