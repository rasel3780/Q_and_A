using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;
namespace Q_A.API.Model
{
    public class Answers
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public string AnswerText { get; set; }
        public string CodeSnippet { get; set; }
        public int MakeByUserID { get; set; }

        public string MakeBy { get; set; }
        public DateTime MakeDate { get; set; }
        public string? AnswerAcceptedBy { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public bool IsAccepted { get; set; }


        public static int SaveAnswer(Answers answers)
        {
            string conString = DbConnection.GetDbConString();
            using (SqlConnection _connection = new SqlConnection(conString))
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandText = "dbo.sp_SaveAnswer";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@QuestionID", answers.QuestionID));
                    cmd.Parameters.Add(new SqlParameter("@AnswerText", answers.AnswerText));
                    cmd.Parameters.Add(new SqlParameter("@CodeSnippet", answers.CodeSnippet));
                    cmd.Parameters.Add(new SqlParameter("@MakeByUserID", answers.MakeByUserID));
                    cmd.Parameters.Add(new SqlParameter("@MakeDate", answers.MakeDate));
                    cmd.Parameters.Add(new SqlParameter("@AnswerAcceptedBy", answers.AnswerAcceptedBy));
                    cmd.Parameters.Add(new SqlParameter("@AcceptedDate", answers.AcceptedDate));

                    int res = cmd.ExecuteNonQuery();
                    return res;

                }
            }
        }

        public static async Task<List<Answers>> GetAnsByQuesId(int quesId)
        {
            List<Answers> ansList = new List<Answers>();
            string conString = DbConnection.GetDbConString();

            using (SqlConnection _connection = new SqlConnection(conString))
            {
                await _connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandText = "dbo.sp_GetAnsByQuesId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@QuestionID", quesId));

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                try
                                {
                                    Answers ans = new Answers
                                    {
                                        AnswerID = Convert.ToInt32(reader["AnswerID"]),
                                        QuestionID = Convert.ToInt32(reader["QuestionID"]),
                                        CodeSnippet = reader["CodeSnippet"].ToString(),
                                        AnswerText = reader["AnswerText"].ToString(),
                                        MakeBy = reader["UserName"].ToString(),
                                        MakeDate = Convert.ToDateTime(reader["MakeDate"]),
                                        IsAccepted = Convert.ToBoolean(reader["IsAccepted"]),
                                        AnswerAcceptedBy = reader["AnswerAcceptedBy"].ToString(),
                                        AcceptedDate = Convert.ToDateTime(reader["AcceptedDate"])
                                    };

                                    ansList.Add(ans);
                                }
                                catch (Exception ex)
                                {
                                    

                                }
                            }
                        }
                    }
                }
            }
            return ansList;
        }

        public static async Task<bool> AcceptAnswer(int answerId, string acceptedBy)
        {
            string conString = DbConnection.GetDbConString();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("sp_AcceptAnswer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@AnswerID", answerId));
                    cmd.Parameters.Add(new SqlParameter("@AcceptedBy", acceptedBy));
                    int result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }
    }
}
