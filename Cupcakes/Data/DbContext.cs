using Microsoft.Data.Sqlite;
using Cupcakes.Models;

namespace Cupcakes.Data
{
    //
    // This is a utility class to execute database CRUD function
    //
    public static class DbContext
    {
        // Get a cupcake by Id
        public static Cupcake GetCupcakeById(int cupcakeId)
        {
            Cupcake cupcake = new Cupcake();

            //
            // Query the database to get the cupcake by id
            //

            // Create connection to database
            SqliteConnection connection = new SqliteConnection("Data Source= Data/Cupcakes.db");

            // Open the connection

            connection.Open();

            // Run SQL to get a cupcake
            string sql = "SELECT CupcakeId, Name, ImageFilename, Description, Price FROM Cupcake WHERE CupcakeId = @CupcakeId";

            // Create a "command" (sql to execute in database) to execute SQL
            SqliteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            // Basically do a find and replace in the sql string
            cmd.Parameters.AddWithValue("@CupcakeId", cupcakeId);

            // Execute the command (use a Data Reader so we can read() the rows returned by the command)
            SqliteDataReader reader = cmd.ExecuteReader();

            // Read through the result (if any)
            if (reader.Read())
            {
                // Read data from the result
                cupcake.CupcakeId = reader.GetInt32(0);
                cupcake.Name = reader.GetString(1);
                if (!reader.IsDBNull(2))
                {
                    cupcake.ImageFilename = reader.GetString(2);
                }
                cupcake.Description = reader.GetString(3);
                cupcake.Price = reader.GetDecimal(4);
            }
            else
            {
                // Handle the case where no data was found
                // You can throw an exception or return null, depending on your needs
                // For now, I'll return null
                cupcake = null;
            }

            // Close the connection
            connection.Close();

            return cupcake;
        }

        // Get all cupcakes in the table
        public static List<Cupcake> GetAllCupcakes()
        {
            List<Cupcake> cupcakes = new List<Cupcake> ();

            
            // Query the database to get the cupcakes
            

            // Create connection to database
            SqliteConnection connection = new SqliteConnection("Data Source= Data/Cupcakes.db");

            // Open the connection
            connection.Open();

            // Run SQL to get all the cupcakes
            string sql = "SELECT CupcakeId, Name, ImageFilename, Description, Price FROM Cupcake";

            // Create a "command" (sql to execute in database) to execute SQL
            SqliteCommand cmd = connection.CreateCommand ();
            cmd.CommandText = sql;

            // Execute the command (use a Data Reader so we can read() the rows returned by the command)
            SqliteDataReader reader = cmd.ExecuteReader ();

            // Read through the results
            while(reader.Read())
            {
                // For each database row, create a Cupcake object and add to the Cupcake list
                Cupcake cupcake = new Cupcake ();
                cupcake.CupcakeId = reader.GetInt32(0);
                cupcake.Name = reader.GetString(1);
                if (!reader.IsDBNull(2))
                {
                    cupcake.ImageFilename = reader.GetString(2);
                }
                cupcake.Description = reader.GetString(3); 
                cupcake.Price = reader.GetDecimal(4);


                // Add this cupcake to list
                cupcakes.Add(cupcake);
            }

            // Close the connection
            connection.Close();

            return cupcakes;
        }

        // Adds a new cupcake object to the table
        public static void AddNewCupcake(Cupcake cupcake)
        {
            // Create connection to database
            SqliteConnection connection = new SqliteConnection("Data Source= Data/Cupcakes.db");

            // Open the connection
            connection.Open();

            // Run SQL to insert cupcake into cupcake table
            // Use command parameters to add the user's input to the SQL
            string sql = "INSERT INTO Cupcake(Name, ImageFilename, Description, Price) VALUES (@Name, @ImageFilename, @Description, @Price)";

            SqliteCommand cmd = connection.CreateCommand();
            
            cmd.CommandText = sql;

            // Basically do a find and replace in the sql string
            cmd.Parameters.AddWithValue("@Name", cupcake.Name);
            cmd.Parameters.AddWithValue("@ImageFilename", cupcake.ImageFilename);
            cmd.Parameters.AddWithValue("@Description", cupcake.Description);
            cmd.Parameters.AddWithValue("@Price", cupcake.Price);


            // Execute the command
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        // Remove "cupcake" from the table
        public static void RemoveCupcake(int cupcakeId)
        {
            // Create connection to database
            SqliteConnection connection = new SqliteConnection("Data Source=Data/Cupcakes.db");

            // Open the connection
            connection.Open();

            // Run SQL to remove cupcake from cupcake table
            // Use command parameters to add the user's input to the SQL
            string sql = "DELETE FROM Cupcake WHERE CupcakeId = @CupcakeId";

            SqliteCommand cmd = connection.CreateCommand();

            cmd.CommandText = sql;

            // Basically do a find and replace in the sql string
            cmd.Parameters.AddWithValue("@CupcakeId", cupcakeId);

            // Execute the command
            cmd.ExecuteNonQuery();

            connection.Close();
        }
    
       
    
    }


}
