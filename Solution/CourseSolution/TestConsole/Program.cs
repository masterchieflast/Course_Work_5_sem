using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

const string connectionString = "server=localhost; Database=PlatinumKitchen; User=SA; Password=Qwer1234; Encrypt=False;";

string sqlQuery = "SELECT * FROM Menu";


using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();

        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader[0].ToString());
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
}