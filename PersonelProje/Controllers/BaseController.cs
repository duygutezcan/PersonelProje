using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _config;



        public BaseController(IConfiguration config)
        {

            _config = config;

        }
        public SqlConnection Connect()
        {
            SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti")); ;
            return con;

        }

    }
}
