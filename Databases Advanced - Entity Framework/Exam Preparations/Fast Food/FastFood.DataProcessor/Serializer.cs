using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FastFood.Data;
using FastFood.DataProcessor.Dto.Export;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
		{
            var orderTypeAsEnum = Enum.Parse<OrderType>(orderType);

            var employee = context.Employees
                                  .ToArray()
                                  .Where(x => x.Name == employeeName)
                                  .Select(x => new
                                  {
                                      Name = x.Name,
                                      Orders = x.Orders.Where(t => t.Type == orderTypeAsEnum)
                                                       .Select(c => new
                                                       {
                                                           Customer = c.Customer,
                                                           Items = c.OrderItems.Select(i => new
                                                           {
                                                               Name = i.Item.Name,
                                                               Price = i.Item.Price,
                                                               Quantity = i.Quantity
                                                           })
                                                           .ToArray(),
                                                           TotalPrice = c.TotalPrice
                                                       })
                                                       .OrderByDescending(t => t.TotalPrice)
                                                       .ThenByDescending(t => t.Items.Length)
                                                       .ToArray(),
                                      TotalMade = x.Orders
                                                        .Where(t => t.Type == orderTypeAsEnum)
                                                        .Sum(t => t.TotalPrice)
                                  })
                                  .FirstOrDefault();

            var jsonString = JsonConvert.SerializeObject(employee, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
		}

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            var categoriesArray = categoriesString.Split(',');

            var categories = context.Categories
                                    .Where(x => categoriesArray.Any(s => s == x.Name))
                                    .Select(d => new CategoryDto
                                    {
                                        Name = d.Name,
                                        MostPopularItem = d.Items.Select(z => new MostPopularItemDto
                                        {
                                            Name = z.Name,
                                            TotalMade = z.OrderItems.Sum(x => x.Item.Price * x.Quantity),
                                            TimesSold = z.OrderItems.Sum(x => x.Quantity)
                                        })
                                        .OrderByDescending(x => x.TotalMade)
                                        .ThenByDescending(x => x.TimesSold)
                                        .FirstOrDefault()
                                    })
                                    .OrderByDescending(x => x.MostPopularItem.TotalMade)
                                    .ThenByDescending(x => x.MostPopularItem.TimesSold)
                                    .ToArray();

            StringBuilder sb = new StringBuilder();

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));

            serializer.Serialize(new StringWriter(sb), categories, xmlNamespaces);

            return sb.ToString().TrimEnd();
		}
	}
}