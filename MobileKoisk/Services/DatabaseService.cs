using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MobileKoisk.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cms;
namespace MobileKoisk.Services
{
	public class DatabaseService
	{
		private readonly string _connectionString;

		public DatabaseService(string server, string database, string userId, string password)
		{
			_connectionString = $"Server={server};Database={database};User ID={userId};Password={password}";
		}

		public async Task<List<Product>> GetShoppingItemsAsync()
		{
			var products = new List<Product>();

			//connection with database 
			using (var connection = new MySqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				//query to retrieve shopping items
				string query = "SELECT ProductId, Name, Description, ImageUrl, Price FROM products";

				using (var cmd = new MySqlCommand(query, connection))
				{
					using(var reader = await cmd.ExecuteReaderAsync())
					{
						while(await reader.ReadAsync())
						{
							products.Add(new Product
							{
								ProductId = reader.GetInt32("ProductId"),
								ProductName = reader.GetString("ProductName"),
								Description = reader.GetString("Description"),
								ImageUrl = reader.GetString("ImageUrl"),
								Price = reader.GetDecimal("Price")
							});
						}
					}
				}
				return products;
			}
		}
	}
}