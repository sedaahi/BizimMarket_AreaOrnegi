using BizimMarket_AreaOrnegi.Areas.Admin.Models;
using BizimMarket_AreaOrnegi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BizimMarket_AreaOrnegi.Areas.Admin.Controllers
{
    [Area("Admin")]  //Doğru root aktif olsun diye
    public class UrunlerController : Controller
    {
        private readonly BizimMarketContext _db;
        private readonly IWebHostEnvironment _env; //siteni nereye yükledin o bilgileri içerir

        public UrunlerController(BizimMarketContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Urunler.Include(x => x.Kategori).ToList());
        }
        public IActionResult Yeni()
        {
            ViewBag.Kategoriler = _db.Kategoriler
                .Select(x => new SelectListItem(x.Ad, x.Id.ToString()))
                .ToList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(YeniUrunViewModel vm)
        {
            #region Resim Geçerliliği
            //if (vm.Resim != null)  //Eğer Resim Varsa!(null değilse)
            //{
            //    if (!vm.Resim.ContentType.StartsWith("image/"))
            //    {
            //        ModelState.AddModelError("Resim", "Geçersiz resim dosyası");
            //        //Gönderdiği resim dosyası değilse tokatlar bu If'i geçemediği için aşağıya geçemez
            //    }
            //    else if (vm.Resim.Length > 1* 1024 * 1024)  //Eğer resim boyutu 1MB'den büyükse izin verme 1000000
            //    {
            //        ModelState.AddModelError("Resim", "Resim dosyası 1MB'den büyük olamaz!");
            //    }
            //}
            #endregion
            if (ModelState.IsValid)
            {
                #region Resim Kaydetme
                //Kullanıcının jpg mi png mi vs ne yüklediyse ona göre yol ayarlar ve
                //aynı isimde dosya olursa diye benzersiz Id olması için Guid dedik
                string dosyaAdi = null;
                if (vm.Resim != null)
                {
                    dosyaAdi = Guid.NewGuid() + Path.GetExtension(vm.Resim.FileName);
                    string kaydetmeYolu =Path.Combine( _env.WebRootPath, "img","Urunler", dosyaAdi);
                    //_env.WebRootPath url'yi verir (Resmin dosya yolu  )

                    using (FileStream fs = new FileStream(kaydetmeYolu, FileMode.Create))//sana belirttiğim yolda bir dosya oluştur(Yolu açar))
                    {
                        vm.Resim.CopyTo(fs);//Dosyayı gönderir ama Close etmelisin!!! Bu nedenle using kullandık ki kullandıktan sonra çöpe atsın 1 kerelik kullanma
                    } 
                }

                #endregion
                Urun urun = new Urun()
                {
                    Ad = vm.Ad,
                    Fiyat = vm.Fiyat.Value,
                    KategoriId = vm.KategoriId.Value,
                    ResimYolu = dosyaAdi
                };

                _db.Add(urun);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            //Eğer if'e girmezse bir sorun var => buraya gir
            ViewBag.Kategoriler = _db.Kategoriler
                .Select(x => new SelectListItem(x.Ad, x.Id.ToString()))
                .ToList();
            return View();
        }
    }
}
