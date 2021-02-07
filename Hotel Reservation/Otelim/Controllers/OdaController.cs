using Otelim.Models;
using Otelim.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Otelim.Controllers
{

    public class OdaDto
    {
        public int id { get; set; }
        public int tipi { get; set; }
        public string manzara { get; set; }
        public string sigaraicilme { get; set; }
        public int fiyat { get; set; }
        public int odano { get; set; }
        public string durum { get; set; }
        public string kalankisi { get; set; }
    }

    public class OdaController : Controller
    {
        OtelDBEntities4 db = new OtelDBEntities4();
        public ActionResult Index()
        {
            var model = (from a in db.Oda
                         join b in db.Rezervasyonlar on a.id equals b.odaid into bTemp
                         from b in bTemp.DefaultIfEmpty()
                         join c in db.Müşteri on b.musteriid equals c.id into cTemp
                         from c in cTemp.DefaultIfEmpty()

                         select new OdaDto
                         {
                             id = a.id,
                             fiyat = a.fiyat,
                             manzara = a.manzara,
                             odano = a.odano,
                             sigaraicilme = a.sigaraicilme == true ? "Sigara İçilebilir" : "Sigara İçilemez",
                             tipi = a.tipi,
                             durum = b.id > 0 ? "DOLU" : "BOŞ",
                             kalankisi = c.isim + " " + c.soyisim
                         }).ToList();

            return View(model);
        }



        [HttpGet]
        public ActionResult OdaBul()
        {


            return View(new OdaBulFiltre());

        }

        [HttpPost]
        public ActionResult OdaBul(OdaBulFiltre oda)
        {
            //Order by Artan ve Azalan komutu gelecek buna göre sorguyu güncelle

            if (!ModelState.IsValid)
            {
                return View("OdaBul", oda);
            }

            //            from ODA O
            //left
            //            join Rezervasyonlar R on o.id = R.odaid

            //where ((R.giristarihi <= GETDATE() and GETDATE()>= cikistarihi) or(r.cikistarihi is null))

            var query = from a in db.Oda
                        join b in db.Rezervasyonlar on a.id equals b.odaid into bTemp
                        from b in bTemp.DefaultIfEmpty()

                        join c in db.Müşteri on b.musteriid equals c.id into cTemp
                        from c in cTemp.DefaultIfEmpty()

                        where
                        (b.giristarihi <= oda.GirisTarihi && oda.CikisTarihi >= b.cikistarihi && a.tipi >= oda.Tipi) || (a.tipi >= oda.Tipi)

                        select new OdaDto
                        {
                            id = a.id,
                            fiyat = a.fiyat,
                            manzara = a.manzara,
                            odano = a.odano,
                            sigaraicilme = a.sigaraicilme == true ? "Sigara İçilebilir" : "Sigara İçilemez",
                            tipi = a.tipi,
                            durum = b.id > 0 ? "DOLU" : "BOŞ",
                            kalankisi=c.isim + " " + c.soyisim
                        };

            //DATABASE LOGIC 
            switch (oda.OrderBy)
            {
                case "Artan":
                    query = query.OrderBy(t => t.fiyat);

                    break;

                case "Azalan":
                    query = query.OrderByDescending(t => t.fiyat);

                    break;

            }

            //SQL kodları herzaman ("")çift tırnak içine yazılır..
            //string vb. göstereceğimiz zaman ('') tek tırnak kullanmalıyız...



            var sqlStr = $"{query}";

            var sqlResult = query.ToList();

            return View("Index", sqlResult);

        }

        [HttpGet]
        public ActionResult YeniRezervasyon(int odaid)
        {
            var oda = db.Oda.First(e => e.id == odaid);

            var musteriler = db.Müşteri.ToList();

            var model = new odaKayitFiltre
            {
                odaid = odaid,
                oda = oda,
                musteriid = 0,
                Müşteriler = musteriler
            };

            return View("MusteriEkle", model);
        }

        [HttpPost]
        public ActionResult YeniRezervasyon(odaKayitFiltre request)
        {

            ///DATABASE KAYIT İŞLEMLERİ
            db.Rezervasyonlar.Add(new Rezervasyonlar()
            {
                musteriid = request.musteriid,
                odaid = request.odaid,
                durum = "DOLU",
                giristarihi = request.GirisTarihi,
                cikistarihi = request.CikisTarihi,
            });

            db.SaveChanges();

            return Redirect("Index");
        }

    }
}