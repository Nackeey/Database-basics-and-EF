namespace ProductShop.App
{
    using AutoMapper;

    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

            //ImportUsers(context);

            //ImportCategoryProducts(context);

            //ExportProductInRange(context);

            //ExportUsersSoldProducts(context);

            //ExportCategoriesByProducts(context);

            var users = new
            {
                usersCount = context.Users.Count(),
                users = context.Users
                               .Where(x => x.ProductsSold.Count > 0)
                               .OrderByDescending(x => x.ProductsSold.Count)
                               .ThenBy(x => x.LastName)
                               .Select(c => new
                               {
                                   firstName = c.FirstName,
                                   lastName = c.LastName,
                                   age = c.Age,
                                   soldProducts = c.ProductsSold.Select(p => new
                                   {
                                       count = c.ProductsSold.Count,
                                       products = c.ProductsSold.Select(k => new
                                       {
                                           name = k.Name,
                                           price = k.Price
                                       })
                                       .ToArray()
                                   })
                                   .ToArray()
                               })
                               .ToArray()
            };

            var jsonString = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/users-and-products.json", jsonString);
        }

        private static void ExportCategoriesByProducts(ProductShopContext context)
        {
            var categories = context.Categories
                                                .OrderByDescending(x => x.CategoryProducts.Count)
                                                .Select(c => new
                                                {
                                                    category = c.Name,
                                                    productsCount = c.CategoryProducts.Count,
                                                    averagePrice = c.CategoryProducts.Select(p => p.Product.Price).DefaultIfEmpty(0).Average(),
                                                    totalRevenue = c.CategoryProducts.Sum(s => s.Product.Price)
                                                })
                                                .ToArray();

            var jsonString = JsonConvert.SerializeObject(categories, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/categories-by-products.json", jsonString);
        }

        private static void ExportUsersSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                               .Where(x => x.ProductsSold.Count > 0)
                               .OrderBy(x => x.LastName)
                               .ThenBy(x => x.FirstName)
                               .Select(c => new
                               {
                                   firstName = c.FirstName,
                                   lastName = c.LastName,
                                   soldProducts = c.ProductsSold
                                                    .Where(p => p.Buyer != null)
                                                    .Select(p => new
                                                    {
                                                        name = p.Name,
                                                        price = p.Price,
                                                        buyerFirstName = p.Buyer.FirstName,
                                                        buyerLastName = p.Buyer.LastName
                                                    })
                                                    .ToArray()
                               })
                               .ToArray();

            var jsonString = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText("../../../Json/users-sold-products.json", jsonString);
        }

        private static void ExportProductInRange(ProductShopContext context)
        {
            var products = context.Products
                                              .Where(p => p.Price >= 500 && p.Price <= 1000)
                                              .OrderBy(p => p.Price)
                                              .Select(x => new
                                              {
                                                  name = x.Name,
                                                  price = x.Price,
                                                  seller = x.Seller.FirstName + ' ' + x.Seller.LastName ?? x.Seller.LastName
                                              })
                                              .ToArray();

            var jsonProducts = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText("../../../Json/products-in-range.json", jsonProducts);
        }

        private static void ImportCategoryProducts(ProductShopContext context)
        {
            var categoryProducts = new List<CategoryProduct>();

            for (int productId = 1; productId <= 200; productId++)
            {
                var categoryId = new Random().Next(1, 12);

                var categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryId,
                    ProductId = productId
                };

                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
        }

        private static void ImportUsers(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/users.json");

            var deserializedUsers = JsonConvert.DeserializeObject<User[]>(jsonString);

            List<User> users = new List<User>();

            foreach (var user in deserializedUsers)
            {
                if (IsValid(user))
                {
                    users.Add(user);
                }
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }
    }
}
