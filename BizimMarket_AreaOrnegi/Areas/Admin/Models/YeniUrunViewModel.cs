using BizimMarket_AreaOrnegi.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizimMarket_AreaOrnegi.Areas.Admin.Models
{
    public class YeniUrunViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunlu.")]
        public string Ad { get; set; }
        
        [Required(ErrorMessage ="Fiyat alanı zorunlu.")]
        public decimal? Fiyat { get; set; }

        [Required(ErrorMessage = "Kategori alanı zorunlu.")]
        public int? KategoriId { get; set; }

        //Attributes > GecerliResimAttribute'da yaratıldı
        [GecerliResim(MaksimumDosyaBoyutuMb = 2)] //Resim boyutunu ayarladık artık 2MBden büyük resim yüklenemez  
        public IFormFile Resim { get; set; }   //IFromFile HttpRequest ile gönderilen bir dosyayı temsil eder.(Formdan dosya göndermekiçin) 
        //Forma <input type="file" name="Resim" /> demeliyiz buna ulaşmak için (Aşağıdaki linke bak!)
        //https://stackoverflow.com/questions/35379309/how-to-upload-files-in-asp-net-core
    }
}
