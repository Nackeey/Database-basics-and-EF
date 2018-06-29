using _1._Initial_Setup;
using System;
using System.Data.SqlClient;

namespace _3._Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);

            connection.Open();

            string villainName = GetVillainName(villainId, connection);

            if (villainName == null)
            {
                Console.WriteLine($"No villain with ID {villainId} exists in the database.");
            }
            else
            {
                Console.WriteLine($"Villain: {villainName}");

                PrintNames(villainId, connection);
            }

            connection.Close();
        }

        private static void PrintNames(int villainId, SqlConnection connection)
        {
            string getMinions = $"SELECT m.Name, m.Age FROM Minions AS m JOIN MinionsVillains AS mv ON mv.MinionId = m.Id WHERE mv.VillainId = {villainId} ORDER BY m.Name ASC";
            using (SqlCommand command = new SqlCommand(getMinions, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        int counter = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{counter++}. {reader[0]} {reader[1]}");
                        }
                    }
                }
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
            string name = $"SELECT Name FROM Villains WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(name, connection))
            {
                command.Parameters.AddWithValue("@Id", villainId);
                return (string)command.ExecuteScalar();
            }
        }
    }
}
