
using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.Data;
using PersonelProje.DTO;
using PersonelProje.Models;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class PersonelController : BaseController
    {
        PersonelModel _model;

        public PersonelController(IConfiguration config, PersonelModel model) : base(config)
        {
            _model = model;
        }
        public List<Ulke> Ulkeler()
        {
            return Connect().Query<Ulke>($"select * from Ulke ").ToList();
        }
        public List<Sehir> Sehirler()
        {
            return Connect().Query<Sehir>($"select * from Sehir ").ToList();
        }
        public Personel PersonelBul(int Id)
        {
            return Connect().Query<Personel>($"select * from personel where Id = {Id}").FirstOrDefault();
        }
        public IActionResult Liste()
        {
            string qry = "select p.Id,p.Ad, Ad + '' + Soyad Adsoy , UlkeAd , SehirAd from Personel p\r\ninner join Sehir s on s.Id = p.SehirId\r\ninner join Ulke u on u.Id = p.UlkeId";
            var Liste = Connect().Query<PersonelDTO>(qry).ToList();
            return View(Liste);
        }

        public IActionResult Guncel(int Id)
        {
            _model.Personel = PersonelBul(Id);
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Güncelleme İşlemi";
            _model.BtnText = "Güncelle";
            _model.BtnClass = "btn btn-danger";
            return View("Genel", _model);

        }


        [HttpPost]
        public IActionResult Guncel(PersonelModel model)
        {
            Personel personel = model.Personel;
            string qry = "update personel set Ad = @Ad , Soyad = @Soyad , Maas = @Maas , " +
                "UlkeId=@UlkeId, SehirId=@SehirId where Id = @Id";
            Connect().ExecuteScalar<int>(qry, personel);

            return RedirectToAction("Liste");
        }

        public IActionResult Giris()
        {
            _model.Personel =new Personel();
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Giriş İşlemi";
            _model.BtnText = "Yeni Giriş";
            _model.BtnClass = "btn btn-primary";
            return View("Genel", _model);

            
        }

        [HttpPost]
        public IActionResult Giris(PersonelModel model)
        {
            Personel personel = model.Personel;
            string qry = "insert into personel(Ad,Soyad,Maas,UlkeId,SehirId) values(@Ad, @Soyad, @Maas, @UlkeId, @SehirId)";

            Connect().ExecuteScalar<int>(qry, personel);

            return RedirectToAction("Liste");
        }

        public IActionResult Sil(int Id)
        {
            _model.Personel = PersonelBul(Id);
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Silme İşlemi";
            _model.BtnText = "Sil";
            _model.BtnClass = "btn btn-danger";
            return View("Genel", _model);

        }

        [HttpPost]
        public IActionResult Sil(PersonelModel model)
        {
            Personel personel = model.Personel;
            string qry = "delete from personel where Id=@Id";

            Connect().ExecuteScalar<int>(qry, personel);

            return RedirectToAction("Liste");
        }
    }



}
