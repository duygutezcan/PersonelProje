using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace PersonelProje.Controllers
{
   
    

    public class UlkeController : BaseController
    {
        public UlkeController(IConfiguration config) : base(config)
        {
        }

        public Ulke UlkeBul(string Id)
        {
            
            string qry = $"Select * from Ulke where Id = '{Id}'";
            return Connect().Query<Ulke>(qry).FirstOrDefault();
       
            
        }


        public IActionResult Liste()
        {
 
            //var con = Connect();
            string qry = "Select * from Ulke";
            var ulkeler = Connect().Query<Ulke>(qry).ToList();
            return View(ulkeler);
        }

        public IActionResult Guncel(string Id)
        {
            return View(UlkeBul(Id));
            //SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            //string qry = $"select * from Ulke where Id = '{Id}'";
            //var secUlke = con.Query<Ulke>(qry).FirstOrDefault();
            //return View(secUlke);

        }
        [HttpPost]
        public IActionResult Guncel(Ulke ulke)
        {
            //var con = Connect();

            string qry = $"update ulke set UlkeAd = @UlkeAd where Id = '{ulke.Id}'";    // @Id de yazılabilir

            Connect().ExecuteScalar<int>(qry, ulke); // update, delete ve insert için kullanılır
            //string qry = $"update ulke set UlkeAd = @UlkeAd where Id = @Id";
            //DynamicParameters par = new DynamicParameters();
            //par.Add("@UlkeAd", ulke.UlkeAd);
            //par.Add("@Id", ulke.Id);
            //con.ExecuteScalar<int>(qry,par);
            return RedirectToAction("Liste");

        }
        public IActionResult Sil(string Id)
        {
            //var con = Connect();
            string qry = $"select * from Ulke where Id = '{Id}'";
            var secUlke = Connect().Query<Ulke>(qry).FirstOrDefault();
            return View(secUlke);
        }
        [HttpPost]
        public IActionResult Sil(Ulke ulke)
        {
            //var con = Connect();

            string qry = $"delete from ulke where Id = '{ulke.Id}'";    

            var x = Connect().ExecuteScalar<int>(qry, ulke);
            return RedirectToAction("Liste");

        }
        public IActionResult Giris(Ulke yeniUlke, bool d) // bool d iki metod aynı olmasının diye eklendi.Kullanılmayacak hiç bir zaman
        {
            return View(yeniUlke);
        }
        [HttpPost]
        public IActionResult Giris(Ulke ulke)
        {
            //var con = Connect();

            string qry = $"INSERT INTO ulke (Id,UlkeAd) VALUES (@Id,@UlkeAd)";

            Connect().ExecuteScalar<int>(qry,ulke);
            return RedirectToAction("Liste");

        }

    }
}
