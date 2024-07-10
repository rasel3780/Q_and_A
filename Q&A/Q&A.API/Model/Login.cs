using System.Data.SqlClient;
using System.Data;

namespace Q_A.API.Model
{
    public class Login
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        
    }
}
