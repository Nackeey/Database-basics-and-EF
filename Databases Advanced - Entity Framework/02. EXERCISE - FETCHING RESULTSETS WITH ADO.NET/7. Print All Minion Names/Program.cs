using _1._Initial_Setup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _7._Print_All_Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);

            connection.Open();

            var minions = GetMinions(connection);

            PrintNames(minions);

            connection.Close();
        }

        private static void PrintNames(List<string> minions)
        {
            int counter = minions.Count - 1;
            for (int i = 0; i < minions.Count / 2; i++)
            {
                Console.WriteLine(minions[i]);
                Console.WriteLine(minions[counter--]);
            }
        }

        private static List<string> GetMinions(SqlConnection connection)
        {
            var namesList = new List<string>();
            
            string getNames = "SELECT Name FROM Minions";
            using (SqlCommand command = new SqlCommand(getNames, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        namesList.Add((string)reader[0]);
                    }

                    return namesList;
                }
            }
        }
    }
}
