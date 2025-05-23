using Npgsql;
using System.Data;

namespace KinclongIN.Helpers
{
    public class sqlDBHelper
    {
        private NpgsqlConnection connection;
        private string __constr;

        public sqlDBHelper(string pConstr)
        {
            __constr = pConstr;
            connection = new NpgsqlConnection();
            connection.ConnectionString = __constr;
        }
        public NpgsqlCommand getNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void closeConnection()
        {
            connection.Close();
        }
    }
}
