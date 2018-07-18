using P01_BillsPaymentSystem.Data.Models;

namespace BillsPaymentSystemInitializer
{
    public class UserInitializer
    {
        public static User[] GetUsers()
        {
            User[] users = new User[]
            {
                new User() { FirstName = "Nasko", LastName = "Porodinski", Email = "nako@softuni.bg", Password = "dawdjaw123" },
                new User() { FirstName = "Barry", LastName = "Black", Email = "jan@softuni.bg", Password = "flfhk1122" },
                new User() { FirstName = "Maleto", LastName = "Kofrajista", Email = "niee@softuni.bg", Password = "123189122" },
                new User() { FirstName = "Simo", LastName = "Metroto", Email = "okoo@softuni.bg", Password = "bdf23255" },
                new User() { FirstName = "Ceco", LastName = "Voennia", Email = "kaboom@softuni.bg", Password = "adagg3111" }
            };

            return users;
        }
    }
}
