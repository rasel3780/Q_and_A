using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Data;

namespace Q_A.API.Model
{
    public class Questions
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Question { get; set; }
        public string MakeBy { get; set; }
        public DateTime MakeDate { get; set; }
        public int UserID { get; set; }
        public List<Answers> AnswersList { get; set; }

        public Questions()
        {
            AnswersList = new List<Answers>();
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
                                Question = reader["Question"].ToString(),
                                MakeBy = reader["MakeBy"].ToString(),
                                MakeDate = Convert.ToDateTime(reader["MakeDate"]),
                                UserID = Convert.ToInt32(reader["UserID"])
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
                                    Question = reader["Question"].ToString(),
                                    MakeBy = reader["MakeBy"].ToString(),
                                    MakeDate = Convert.ToDateTime(reader["MakeDate"]),
                                    UserID = Convert.ToInt32(reader["UserID"])
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
