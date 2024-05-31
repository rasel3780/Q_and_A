using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Data;

namespace Q_A.API.Model
{
    public class Questions
    {
        public int QuestionID { get; set; }
        public string Category { get; set; }
        public string Question { get; set; }
        public string MakeBy { get; set; }
        public DateTime MakeDate { get; set; }
        public int UserID { get; set; }
        public List<Questions> QuestionList { get; set; }

        public Questions()
        {
            QuestionList = new List<Questions>();
        }

        public static List<Questions> GetAllQuestion()
        {
            List<Questions> quesList = new List<Questions>();
            string conString = DbConnection.GetDbConString();

            SqlConnection _connection = new SqlConnection(conString);
            _connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "sp_GetQuestionList";
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Questions obj = new Questions();
                    obj.QuestionID = (int)reader["QuestionID"];
                    obj.Category = reader["Category"].ToString();
                    obj.Question = reader["Question"].ToString();
                    obj.MakeBy = reader["MakeBy"].ToString();
                    obj.MakeDate = (DateTime)reader["MakeDate"];
                    obj.UserID = (int)reader["UserID"];

                    quesList.Add(obj);
                }    
            }
            cmd.Dispose();
            _connection.Close();
            return quesList;
        }


    }
}
