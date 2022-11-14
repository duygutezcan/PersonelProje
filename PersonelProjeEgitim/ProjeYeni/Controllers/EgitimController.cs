using Dapper;
using Microsoft.AspNetCore.Mvc;
using ProjeYeni.Data;
using ProjeYeni.Models;
using System.Data.SqlClient;

namespace ProjeYeni.Controllers
{
    public class EgitimController : Controller
    {
        IConfiguration _config;
        EgitimModel _model;
        public EgitimController(IConfiguration config, EgitimModel model)
        {
            _config = config;
            _model = model;
        }

        public SqlConnection Connect()
        {
            return new SqlConnection(_config.GetConnectionString("Baglanti"));
        }

        public Egitim EgitimBul(int Id)
        {
            string qry = $"select * from egitim where Id = '{Id}'";
            return Connect().Query<Egitim>(qry).FirstOrDefault();
        }

        public IActionResult Liste()
        {
            string qry = $"select * from egitim  where deleted = 0 order by Id";
            var egitimler = Connect().Query<Egitim>(qry).ToList();
            return View(egitimler);
        }

        public IActionResult Guncel(int Id)
        {
            return View(EgitimBul(Id));
        }
        [HttpPost]
        public IActionResult Guncel(Egitim egitim)
        {
            string qry = $"update egitim set EgitimAd = @EgitimAd where Id = @Id";
            Connect().ExecuteScalar<int>(qry, egitim);
            return RedirectToAction("Liste");
        }

        public IActionResult Sil(int Id)
        {
            return View(EgitimBul(Id));
        }

        [HttpPost]
        public IActionResult Sil(Egitim egitim)
        {
            string qry = $"delete from egitim where Id = '{egitim.Id}'";
            Connect().ExecuteScalar<int>(qry, egitim);
            return RedirectToAction("Liste");
        }

        public IActionResult Ekle(Egitim yeniegitim, bool d)
        {
            _model.Egitim = yeniegitim;
            string qry = $"select * from egitim order by Id";
            _model.SonId = Connect().Query<Egitim>(qry).ToList().Max(x => x.Id)+1; // Egitim listesi içerisinden Max Id yi buluyoruz.
            return View(_model);
        }

        [HttpPost]
        public IActionResult Ekle(EgitimModel model)
        {
            var egitim = model.Egitim;
            var Id = model.SonId;
            string qry = $"insert into egitim (Id, EgitimAd, Deleted) values({Id}, @EgitimAd, 0)";
            Connect().ExecuteScalar<int>(qry, egitim);
            return RedirectToAction("Liste");
        }
    }
}
