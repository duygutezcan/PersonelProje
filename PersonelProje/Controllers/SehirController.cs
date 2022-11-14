using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.Data;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class SehirController : Controller
    {
    IConfiguration _config;
        public SehirController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connect()
        {
            SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti")); ;
            return con;

        }

        public Sehir SehirBul(int a)
        {
            //var con = Connect();
            string qry = "Select * from Sehir where Id = @Id";
            return Connect().Query<Sehir>(qry).FirstOrDefault();

            //bu şekilde metodlada oluşturulabilir.
        }
        public IActionResult Liste()
        {
            //var con = Connect();
            string qry = "Select * from Sehir";
            var sehirler = Connect().Query<Sehir>(qry).ToList();
            return View(sehirler);
        }

        public IActionResult Guncel(int Id)
        {
            return View(SehirBul(Id));
            //SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            //string qry = $"select * from Ulke where Id = '{Id}'";
            //var secUlke = con.Query<Ulke>(qry).FirstOrDefault();
            //return View(secUlke);

        }
        [HttpPost]
        public IActionResult Guncel(Sehir sehir)
        {
            //var con = Connect();

            string qry = $"update sehir set SehirAd = @SehirAd where Id = '{sehir.Id}'";    // @Id de yazılabilir

            Connect().ExecuteScalar<int>(qry, sehir); // update, delete ve insert için kullanılır
            //string qry = $"update ulke set UlkeAd = @UlkeAd where Id = @Id";
            //DynamicParameters par = new DynamicParameters();
            //par.Add("@UlkeAd", ulke.UlkeAd);
            //par.Add("@Id", ulke.Id);
            //con.ExecuteScalar<int>(qry,par);
            return RedirectToAction("Liste");

        }
        public IActionResult Sil(int Id)
        {
            //var con = Connect();
            string qry = $"select * from Sehir where Id = '{Id}'";
            var secSehir = Connect().Query<Sehir>(qry).FirstOrDefault();
            return View(secSehir);
        }
        [HttpPost]
        public IActionResult Sil(Sehir sehir)
        {
            //var con = Connect();

            string qry = $"delete from sehir where Id = '{sehir.Id}'";

            var x = Connect().ExecuteScalar<int>(qry, sehir);
            return RedirectToAction("Liste");

        }
        public IActionResult Giris(Sehir yeniSehir, bool d) // bool d iki metod aynı olmasının diye eklendi.Kullanılmayacak hiç bir zaman
        {
            return View(yeniSehir);
        }
        [HttpPost]
        public IActionResult Giris(Sehir sehir)
        {
            //var con = Connect();

            string qry = $"INSERT INTO sehir (Id,SehirAd) VALUES (@Id,@SehirAd)";

            Connect().ExecuteScalar<int>(qry, sehir);
            return RedirectToAction("Liste");

        }

    }
}
