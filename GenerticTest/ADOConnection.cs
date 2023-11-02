using MySql.Data.MySqlClient;
using System;

namespace GenerticTest
{
    internal static class ADOConnection
    {
        // ---->>>> parameter for string provider
        private static string host = "localhost";
        private static string database = "generictest";
        private static string userDB = "root";
        private static string password = "Kings600.";
        // <<<<----

        public static MySqlConnection conn = new MySqlConnection();

        // this string is created this way to give the option of inserting parameters differently incase it should be neccesary to split database
        // information from the program
        public static string ConnectionString = $"server={host};Database={database};User ID={userDB};Password={password}";

        /// <summary>
        /// This function opens for the connection to the database.
        /// </summary>
        /// <returns></returns>
        public static bool Open()
        {
            try
            {
                // Here we insert the connection string to the mysqlconnection
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
