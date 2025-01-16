using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MobileKoisk.Models;
using MySql.Data.MySqlClient;

namespace MobileKoisk.Services
{
	internal class DatabaseService
	{
		//storedatabase connection string
		private readonly string _connectionString;

		//constructor to initialize the connection string with database credentials
		public DatabaseService(string server, string database, string userId, string password)
		{
			//format the connection string with provided details
			_connectionString = $"Server={server};Database={database};User ID={userId};Password={password}";
		}

		//ferch users asynchronously from the database 
		public async Task<List<User>> GetUsersAsync()
		{
			//store retrieved user data
			var users = new List<User>();

			//create connection using connectionstring
			using (var connection = new MySqlConnection(_connectionString))
			{
				//open the connection asynchronously 
				await connection.OpenAsync();

				//SQL query to retriev0e userdata
				string query = "SELECT Id, Name, Email From Users";

				//execute query
				using (var cmd = new MySqlCommand(query, connection))
				{
					using (var reader = await cmd.ExecuteReaderAsync())
					{
						//iterate through the result set
						while (await reader.ReadAsync())
						{
							users.Add(new User
							{
								Id = reader.GetInt32("Id"),
								Name = reader.GetString("Name"),
								Email = reader.GetString("Email")
							});
						}
					}
				}
				
			}
			return users;
		}
		//insert new user into the database 
		public async Task<int> AddUserAsync(User user)
			{
				using (var connection = new MySqlConnection(_connectionString))
				{
					//open connection
					await connection.OpenAsync();

					//sql query to insert data
					string query = "INSERT INTO Users(Name, Email) VALUES (@Name, @Email)";

					using (var cmd = new MySqlCommand(query, connection))
					{
						cmd.Parameters.AddWithValue("@Name", user.Name);
						cmd.Parameters.AddWithValue("@Email", user.Email);

						//execute query the query and return the number of affected rows 
						return await cmd.ExecuteNonQueryAsync();
					}
				}
			}
	}
}
