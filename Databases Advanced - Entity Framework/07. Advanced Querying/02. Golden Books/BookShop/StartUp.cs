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

                string goldenBooksTitles = GetGoldenBooks(db);

                Console.WriteLine(goldenBooksTitles);
            }
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            string stringEditionType = "Gold";
            var sb = new StringBuilder();

            EditionType edition;

            if (Enum.TryParse(stringEditionType, out edition))
            {
                var goldenBooks = context.Books
                    .OrderBy(x => x.BookId)
                    .Where(x => x.EditionType == edition && x.Copies < 5000)
                    .Select(x => x.Title)
                    .ToArray();

                foreach (var title in goldenBooks)
                {
                    sb.AppendLine(title);
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
