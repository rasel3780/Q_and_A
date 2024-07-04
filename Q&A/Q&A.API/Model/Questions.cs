using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Q_A.API.Model
{
    public class Questions
    {
        public int QuestionID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string QuestionText { get; set; }
        
    
        public int MakeByUserId { get; set; }

        public string MakeBy { get; set; }
     
        public DateTime MakeDate { get; set; }
        public int UserID { get; set; }
        public string CodeSnippet { get; set; }
        public List<Answers> AnswersList { get; set; }

        public Questions()
        {
            AnswersList = new List<Answers>();
        }

        public static int SaveQuestion(Questions question)
        {
            string conString = DbConnection.GetDbConString();
            using (SqlConnection _connection = new SqlConnection(conString))
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandText = "dbo.sp_SaveQuestion";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@Title", question.Title));
                    cmd.Parameters.Add(new SqlParameter("@Category", question.Category));
                    cmd.Parameters.Add(new SqlParameter("@QuestionText", question.QuestionText));
                    cmd.Parameters.Add(new SqlParameter("@MakeByUserID", question.MakeByUserId));
                    cmd.Parameters.Add(new SqlParameter("@MakeDate", question.MakeDate));
                    cmd.Parameters.Add(new SqlParameter("@CodeSnippet", question.CodeSnippet));

                    int res = cmd.ExecuteNonQuery();
                    return res;

                }
            }
        }

        public static async Task<List<Questions>> GetAllQuestion()
        {
            List<Questions> quesList = new List<Questions>();
            string conString = DbConnection.GetDbConString();

            using (SqlConnection _connection = new SqlConnection(conString))
            {
                await _connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandText = "dbo.sp_GetQuestionList";
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            Questions obj = new Questions
                            {
                                QuestionID = Convert.ToInt32(reader["QuestionID"]),
                                Title = reader["Title"].ToString(),
                                Category = reader["Category"].ToString(),
                                QuestionText = reader["QuestionText"].ToString(),
                                MakeBy = reader["UserName"].ToString(),
                                MakeDate = Convert.ToDateTime(reader["MakeDate"]),
                                CodeSnippet = reader["CodeSnippet"].ToString()
                            };
                            quesList.Add(obj);
                        }
                    }
                }
            }
            return quesList;
        }

        public static async Task<Questions> GetQuesById(int questionID)
        {
            Questions ques = null;
            string conString = DbConnection.GetDbConString();

            using (SqlConnection _connection = new SqlConnection(conString))
            {
                await _connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandText = "dbo.sp_GetQuesById";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@QuestionID", questionID));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())

                            {
                                ques = new Questions
                                {
                                    QuestionID = Convert.ToInt32(reader["QuestionID"]),
                                    Title = reader["Title"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    QuestionText = reader["QuestionText"].ToString(),
                                    MakeBy = reader["UserName"].ToString(),
                                    MakeDate = Convert.ToDateTime(reader["MakeDate"]),
                                    CodeSnippet = reader["CodeSnippet"].ToString()
                                };

                            }
                        }
                    }
                }
            }
            return ques;
        }
    }
}
