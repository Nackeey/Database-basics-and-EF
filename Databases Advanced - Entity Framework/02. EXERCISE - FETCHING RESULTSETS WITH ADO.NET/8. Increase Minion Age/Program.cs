using _1._Initial_Setup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _8._Increase_Minion_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] minionsIds = Console.ReadLine().Split().Select(int.Parse).ToArray();

            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);

            connection.Open();

            UpdateAge(minionsIds, connection);
            var minions = GetNameAndAge(connection);

            foreach (var minion in minions)
            {
                Console.WriteLine(minion);
            }

            connection.Close();
        }

        private static List<string> GetNameAndAge(SqlConnection connection)
        {
            List<string> minions = new List<string>();

            string getNamesAndAges = "SELECT Name, Age FROM Minions";

            using (SqlCommand command = new SqlCommand(getNamesAndAges, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        minions.Add(string.Concat((string)reader[0] + ' ' + reader[1]));
                    }

                    return minions;
                }
            }
        }

        private static void UpdateAge(int[] minionsIds, SqlConnection connection)
        {
            string updateNamesAndAge = "UPDATE Minions SET Age += 1, Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2 , LEN(Name)) WHERE Id = @minionId";

            for (int i = 0; i < minionsIds.Length; i++)
            {
                using (SqlCommand command = new SqlCommand(updateNamesAndAge, connection))
                {
                    command.Parameters.AddWithValue("@minionId", minionsIds[i]);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
