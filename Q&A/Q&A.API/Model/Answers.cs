using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;
namespace Q_A.API.Model
{
    public class Answers
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public string MakeBy { get; set; }
        public DateTime MakeDate { get; set; }
        public string AnswerAcceptedBy { get; set; }
        public DateTime AnswerAcceptedDate { get; set; }
        public int UserID { get; set; }

        public static List<Answers> GetAnsByQuesId(int quesId)
        {
            List<Answers> ansList = new List<Answers>();
            string conString = DbConnection.GetDbConString();

            SqlConnection _connnection = new SqlConnection(conString);
            _connnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connnection;
            cmd.CommandText = "dbo.sp_GetAnsByQuesId";
                
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@QuestionID", quesId));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Answers obj = new Answers();
                    obj.AnswerID = Convert.ToInt32(reader["AnswerID"]);
                    obj.QuestionID = Convert.ToInt32(reader["QuestionID"]); ;
                    obj.Answer = reader["Answer"].ToString();
                    obj.MakeBy = reader["MakeBy"].ToString(); ;
                    obj.MakeDate = Convert.ToDateTime(reader["MakeDate"].ToString()); ;
                    obj.AnswerAcceptedBy = reader["AnswerAcceptedBy"].ToString(); ;
                    obj.AnswerAcceptedDate = Convert.ToDateTime(reader["AnswerAcceptedDate"].ToString()); ;
                    obj.UserID = Convert.ToInt32(reader["UserID"]);

                    ansList.Add(obj);
                }
            }
            cmd.Dispose();
            _connnection.Close();
            return ansList;
        }
    }
}
