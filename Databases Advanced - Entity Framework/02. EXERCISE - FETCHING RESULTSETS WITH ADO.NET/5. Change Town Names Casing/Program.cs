using _1._Initial_Setup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _5._Change_Town_Names_Casing
{
    class Program
    {
        static void Main(string[] args)
        {
            string countryName = Console.ReadLine();

            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);

            connection.Open();

            int countryId = GetCountryId(countryName, connection);
            if (countryId == 0)
            {
                Console.WriteLine($"No town names were affected.");
            }
            else
            {
                int townNumber = GetNumberOfTowns(countryId, connection);

                PrintTownsNames(townNumber, countryId, connection);
            }
            
            connection.Close();
        }

        private static void PrintTownsNames(int townNumber, int countryId, SqlConnection connection)
        {
            string getTowns = "SELECT UPPER(t.Name) FROM Towns AS t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Id = @Id";
            using (SqlCommand command = new SqlCommand(getTowns, connection))
            {
                command.Parameters.AddWithValue("@Id", countryId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine($"No town names were affected.");
                    }
                    else
                    {
                        var list = new List<string>();
                        while (reader.Read())
                        {
                            list.Add((string)reader[0]);
                        }

                        Console.WriteLine($"{townNumber} town names were affected.");
                        Console.WriteLine($"[{string.Join(", ", list)}]");
                    }
                }
            }
        }

        private static int GetNumberOfTowns(int countryId, SqlConnection connection)
        {
            string getCount = "SELECT COUNT(t.Name) FROM Towns AS t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Id = @Id";
            using (SqlCommand command = new SqlCommand(getCount, connection))
            {
                command.Parameters.AddWithValue("@Id", countryId);

                return (int)command.ExecuteScalar();
            }
        }

        private static int GetCountryId(string countryName, SqlConnection connection)
        {
            string getId = "SELECT Id FROM Countries WHERE Name = @Name";
            using (SqlCommand command = new SqlCommand(getId, connection))
            {
                command.Parameters.AddWithValue("@Name", countryName);
                if (command.ExecuteScalar() == null)
                {
                    return 0;
                }

                return (int)command.ExecuteScalar();
            }
        }
    }
}
