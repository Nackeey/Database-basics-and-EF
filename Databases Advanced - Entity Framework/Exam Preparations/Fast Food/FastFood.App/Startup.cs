﻿using System;
using System.Data.SqlClient;
using System.IO;
using AutoMapper;
using FastFood.Data;
using FastFood.DataProcessor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FastFood.App
{
	public class Startup
	{
		public static void Main(string[] args)
		{
			var context = new FastFoodDbContext();

			ResetDatabase(context);

			Console.WriteLine("Database Reset.");

			Mapper.Initialize(cfg => cfg.AddProfile<FastFoodProfile>());

			ImportEntities(context);

			ExportEntities(context);

			BonusTask(context);
		}

		private static void ImportEntities(FastFoodDbContext context, string baseDir = @"C:\Users\s2k\OneDrive\Desktop\Database Advanced - Entity Framework\Exam Preparation\C# DB Advanced Exam - 10.12.2017\01. Model Definition_Project Skeleton\Datasets\")
		{
			const string exportDir = @"C:\Users\s2k\OneDrive\Desktop\Database Advanced - Entity Framework\Exam Preparation\C# DB Advanced Exam - 10.12.2017\01. Model Definition_Project Skeleton\FastFood.App\ImportResults\";

			var employees = DataProcessor.Deserializer.ImportEmployees(context, File.ReadAllText(baseDir + "employees.json"));
			PrintAndExportEntityToFile(employees, exportDir + "Employees.txt");

			var items = DataProcessor.Deserializer.ImportItems(context, File.ReadAllText(baseDir + "items.json"));
			PrintAndExportEntityToFile(items, exportDir + "Items.txt");

			var orders = DataProcessor.Deserializer.ImportOrders(context, File.ReadAllText(baseDir + "orders.xml"));
			PrintAndExportEntityToFile(orders, exportDir + "Orders.txt");
		}

		private static void ExportEntities(FastFoodDbContext context)
		{
			const string exportDir = @"C:\Users\s2k\OneDrive\Desktop\Database Advanced - Entity Framework\Exam Preparation\C# DB Advanced Exam - 10.12.2017\01. Model Definition_Project Skeleton\FastFood.App\ImportResults\";

            var jsonOutput = DataProcessor.Serializer.ExportOrdersByEmployee(context, "Avery Rush", "ToGo");
			Console.WriteLine(jsonOutput);
			File.WriteAllText(exportDir + "OrdersByEmployee.json", jsonOutput);

			var xmlOutput = DataProcessor.Serializer.ExportCategoryStatistics(context, "Chicken,Drinks,Toys");
			Console.WriteLine(xmlOutput);
			File.WriteAllText(exportDir + "CategoryStatistics.xml", xmlOutput);
		}

		private static void BonusTask(FastFoodDbContext context)
		{
			var bonusOutput = DataProcessor.Bonus.UpdatePrice(context, "Cheeseburger", 6.50m);
			Console.WriteLine(bonusOutput);
		}

		private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
		{
			Console.WriteLine(entityOutput);
			File.WriteAllText(outputPath, entityOutput.TrimEnd());
		}

		private static void ResetDatabase(FastFoodDbContext context)
		{
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
		}
	}
}