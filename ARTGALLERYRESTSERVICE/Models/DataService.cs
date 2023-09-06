using System.Data.SqlClient;

namespace ARTGALLERYRESTSERVICE.Models
{
    public class DataService
    {
        IConfiguration config;
        SqlConnection con;
        SqlCommand? cmd;
        public DataService(IConfiguration _config)
        {
            this.config = _config;
            con = new SqlConnection();
            con.ConnectionString = config["ConnectionStrings:cstr"];
        }

        public string? FindUser(ArtGalleryCredentialsModel model)
        {
            string? role = null;
            cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select UserRole from Users where UserName=@user and UserPassword=@pwd";

            cmd.Parameters.AddWithValue("@user", model.UserName);
            cmd.Parameters.AddWithValue("@pwd", model.UserPassword);
            con.Open();
            var result = cmd.ExecuteScalar();
            if (result != null) { role = result.ToString(); }
            con.Close();
            return role;
        }
    }
}
