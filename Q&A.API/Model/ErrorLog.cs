using System.Data.SqlClient;
using System.Data;
namespace Q_A.API.Model
{
    public class ErrorLog
    {
        public static void SaveErrorLog(string fromScreen,string ErrorDescription)
        {
            string conString = DbConnection.GetDbConString();
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.InsErrorLog";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@EventRaiseSceen", fromScreen));
            cmd.Parameters.Add(new SqlParameter("@ErrorDescription", ErrorDescription)); 
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
        }
    }
}
