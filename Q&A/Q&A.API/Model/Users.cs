using System.Data.SqlClient;
using System.Data;

namespace Q_A.API.Model
{
    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static Users AuthenticateUser(Login loginData)
        {
            Users user = null;
            string conString = DbConnection.GetDbConString();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserName", loginData.UserName));
                    cmd.Parameters.Add(new SqlParameter("@Password", loginData.Password));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            user = new Users
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                UserName = reader["UserName"].ToString()
                            };
                        }
                    }
                }
            }
            return user;
        }
        public static bool IsFieldUnique(string field, string value)
        {
            string procedureName = null;
            string parameterName = null;

            if (field.ToLower() == "username")
            {
                procedureName = "dbo.CheckUniqueUsername";
                parameterName = "@Username";
            }
            else if (field.ToLower() == "email")
            {
                procedureName = "dbo.CheckUniqueEmail";
                parameterName = "@Email";
            }

            string conString = DbConnection.GetDbConString();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(parameterName, value));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return !reader.HasRows;
                    }
                }
            }
        }


        public static int Register(Users user)
        {
            string conString = DbConnection.GetDbConString();
            using (SqlConnection _connection = new SqlConnection(conString))
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandText = "SaveUser";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                    cmd.Parameters.Add(new SqlParameter("@EMail", user.Email));
                    cmd.Parameters.Add(new SqlParameter("Password", user.Password));

                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }

        }
    }
}
