using _1._Initial_Setup;
using System;
using System.Data.SqlClient;

namespace _2._Villain_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);

            connection.Open();

            string villainsInfo = "SELECT v.Name, COUNT(mv.MinionId) AS MinionsCount FROM Villains AS v " +
                "JOIN MinionsVillains AS mv ON mv.VillainId = v.Id GROUP BY v.Name HAVING COUNT(mv.MinionId) >= 3 " +
                "ORDER BY MinionsCount DESC";

            using (SqlCommand command = new SqlCommand(villainsInfo, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} - {reader[1]}");
                    }
                }
            }
            connection.Close();
        }
    }
}
