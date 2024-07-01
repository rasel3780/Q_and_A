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

        public static async Task<List<Answers>> GetAnsByQuesId(int quesId)
        {
            List<Answers> ansList = new List<Answers>();
            string conString = DbConnection.GetDbConString();

            using (SqlConnection _connnection = new SqlConnection(conString))
            {
                await _connnection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connnection;
                    cmd.CommandText = "dbo.sp_GetAnsByQuesId";

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@QuestionID", quesId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                Answers obj = new Answers
                                {
                                    AnswerID = Convert.ToInt32(reader["AnswerID"]),
                                    QuestionID = Convert.ToInt32(reader["QuestionID"]),
                                    Answer = reader["Answer"].ToString(),
                                    MakeBy = reader["MakeBy"].ToString(),
                                    MakeDate = Convert.ToDateTime(reader["MakeDate"]),
                                    AnswerAcceptedBy = reader["AnswerAcceptedBy"].ToString(),
                                    AnswerAcceptedDate = Convert.ToDateTime(reader["AnswerAcceptedDate"]),
                                    UserID = Convert.ToInt32(reader["UserID"])
                                };

                                ansList.Add(obj);
                            }
                        }
                    }
                }
            }
            return ansList;
        }
    }
}
