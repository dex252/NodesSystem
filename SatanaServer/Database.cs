using System.Configuration;
using MySql.Data.MySqlClient;

namespace SatanaServer
{
    public class Database
    {

        public readonly MySqlConnection Db = new MySqlConnection(ConfigurationManager
            .ConnectionStrings["Database"].ConnectionString);

        public void OpenDatabase()
        {
            if (Db.State == System.Data.ConnectionState.Closed)
            {
                Db.Open();
            }

        }

        public void CloseDatabase()
        {
            if (Db.State == System.Data.ConnectionState.Open)
            {
                Db.Close();
            }

        }
    }
}
