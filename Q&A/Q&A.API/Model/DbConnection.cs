namespace Q_A.API.Model
{
    public class DbConnection
    {
        public static IConfiguration Configuration { get; set; }

        public static string GetDbConString()
        {
            string conString = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            conString = Configuration["ConString"].ToString();

            return conString;
        }
    }
}
