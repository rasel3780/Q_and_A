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
        public string MakeBy { get; set; }
        public DateTime MakeDate { get; set; }
        public string? AnswerAcceptedBy { get; set; }
        public DateTime? AnswerAcceptedDate { get; set; }
        

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
                                        AnswerAcceptedBy = reader["AnswerAcceptedBy"].ToString(),
                                        AnswerAcceptedDate = Convert.ToDateTime(reader["AcceptedDate"])
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
    }
}
