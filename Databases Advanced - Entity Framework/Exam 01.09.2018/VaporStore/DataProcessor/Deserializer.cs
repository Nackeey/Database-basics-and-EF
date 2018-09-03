namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.Data.Models.ImportDtos;

    public static class Deserializer
    {
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserializedJson = JsonConvert.DeserializeObject<GameDto[]>(jsonString);

            var developers = new List<Developer>();
            var genres = new List<Genre>();
            var tags = new List<Tag>();
            var games = new List<Game>();

            foreach (var gameDto in deserializedJson)
            {
                if (!IsValid(gameDto) || !gameDto.Tags.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var developer = developers.SingleOrDefault(x => x.Name == gameDto.Developer);
                if (developer == null)
                {
                    developer = new Developer
                    {
                        Name = gameDto.Developer
                    };

                    developers.Add(developer);
                }

                var genre = genres.SingleOrDefault(x => x.Name == gameDto.Genre);
                if (genre == null)
                {
                    genre = new Genre
                    {
                        Name = gameDto.Genre
                    };

                    genres.Add(genre);
                }

                var gameTags = new List<Tag>();
                foreach (var tagName in gameDto.Tags)
                {
                    var tag = tags.SingleOrDefault(x => x.Name == tagName);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagName
                        };
                        tags.Add(tag);
                    }

                    gameTags.Add(tag);
                }

                var game = new Game
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    Developer = developer,
                    Genre = genre,
                    GameTags = gameTags.Select(t => new GameTag { Tag = t }).ToArray()
                };

                games.Add(game);

                sb.AppendLine($"Added {gameDto.Name} ({gameDto.Genre}) with {gameDto.Tags.Length} tags");
            }

            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var deserializedJson = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var sb = new StringBuilder();
            var users = new List<User>();

            foreach (var userDto in deserializedJson)
            {
                if (!IsValid(userDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                bool hasInvalidCards = !userDto.Cards.All(IsValid);

                if (hasInvalidCards)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var validCards = new List<Card>();

                foreach (var cardDto in userDto.Cards)
                {
                    var cardType = Enum.Parse<CardType>(cardDto.Type);
                    var card = new Card
                    {
                        Number = cardDto.Number,
                        Cvc = cardDto.Cvc,
                        Type = cardType
                    };

                    validCards.Add(card);
                }

                var user = new User
                {
                    FullName = userDto.FullName,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Age = userDto.Age,
                    Cards = validCards
                };

                users.Add(user);
                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }
            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));
            var deserializedPurchases = (PurchaseDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            var purchases = new List<Purchase>();

            foreach (var purchaseDto in deserializedPurchases)
            {
                if (!IsValid(purchaseDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var game = context.Games.Single(x => x.Name == purchaseDto.Title);
                var card = context.Cards.Include(c => c.User).Single(c => c.Number == purchaseDto.Card);
                var date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                var purchase = new Purchase
                {
                    Game = game,
                    Type = purchaseDto.Type,
                    Card = card,
                    ProductKey = purchaseDto.Key,
                    Date = date
                };

                purchases.Add(purchase);

                sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}