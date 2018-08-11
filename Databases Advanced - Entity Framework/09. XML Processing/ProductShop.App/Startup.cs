using ProductShop.App.Dto;
using System.IO;
using System.Xml.Serialization;
using ProductShop.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace ProductShop.App
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            var users = new UsersDto
            {
                Count = context.Users.Count(),
                Users = context.Users
                               .Where(x => x.ProductSold.Count() > 0)
                               .OrderByDescending(x => x.ProductSold.Count)
                               .ThenBy(x => x.LastName)
                               .Select(x => new UserDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age.ToString(),
                    SoldProducts = new SoldProductDto
                    {
                         Count = x.ProductSold.Count(),
                        ProductDto = x.ProductSold.Select(k => new ProductDto
                        {
                            Name = k.Name,
                            Price = k.Price
                        }).ToArray()
                    }
                }).ToArray()
            };

            var sb = new StringBuilder();

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(UsersDto), new XmlRootAttribute("users"));

            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            File.WriteAllText("../../../Xml/users-and-products.xml", sb.ToString());
        }
    }
}
