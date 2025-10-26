using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace Q_A.API.Model
{
    public class Categories
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }


        public static async Task<List<Categories>> GetAllCategory()
        {
            List<Categories> categoryList = new List<Categories>();
            string conString = DbConnection.GetDbConString();

            using (SqlConnection _connection = new SqlConnection(conString))
            {
                await _connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandText = "dbo.sp_GetCategories";
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            Categories obj = new Categories()
                            {
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                CategoryName = reader["CategoryName"].ToString()
                            };

                            categoryList.Add(obj);
                        }
                    }
                }
            }
            return categoryList;
        }
    }
}
