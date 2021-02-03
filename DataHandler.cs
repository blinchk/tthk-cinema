using System.Data;
using System.Data.SqlClient;

namespace tthk_kinoteater
{
    class DataHandler
    {
        private readonly SqlConnection connection = new SqlConnection(
            @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\AppData\cinema.mdf; Integrated Security = True");
        
        public DataHandler()
        {
            connection.Open();
        }

        private void TryToCloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
