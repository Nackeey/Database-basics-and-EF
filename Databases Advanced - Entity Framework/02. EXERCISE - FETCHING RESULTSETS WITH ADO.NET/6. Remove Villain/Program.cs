using _1._Initial_Setup;
using System;
using System.Data.SqlClient;

namespace _6._Remove_Villain
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputVillainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);

            connection.Open();
            int villainId = GetVillainId(inputVillainId, connection);

            if (villainId == 0)
            {
                Console.WriteLine("No such villain was found.");
            }
            else
            {
                int affectedRows = ReleaseMinions(villainId, connection);
                string villainName = GetVillainName(villainId, connection);
                DeleteVillain(villainName, connection);
                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{affectedRows} minions were released.");
            }

            connection.Close();
        }

        private static void DeleteVillain(string villainName, SqlConnection connection)
        {
            string deleteVillain = "DELETE FROM Villains WHERE Name = @Name";
            using (SqlCommand command = new SqlCommand(deleteVillain, connection))
            {
                command.Parameters.AddWithValue("@Name", villainName);
                command.ExecuteNonQuery();
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
            string getNameById = "SELECT Name FROM Villains WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(getNameById, connection))
            {
                command.Parameters.AddWithValue("@Id", villainId);
                return (string)command.ExecuteScalar();
            }
        }

        private static int ReleaseMinions(int villainId, SqlConnection connection)
        {
            string deleteMinions = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";
            using (SqlCommand command = new SqlCommand(deleteMinions, connection))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                return command.ExecuteNonQuery();
            }
        }

        private static int GetVillainId(int inputVillainId, SqlConnection connection)
        {
            string getId = "SELECT Id FROM Villains WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(getId, connection))
            {
                command.Parameters.AddWithValue("@Id", inputVillainId);

                if (command.ExecuteScalar() == null)
                {
                    return 0;
                }

                return (int)command.ExecuteScalar();
            }
        }
    }
}
