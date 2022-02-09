using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizimMarket_AreaOrnegi.Models
{
    public class HomeViewModel
    {
        public List<Kategori> Kategoriler { get; set; }
        public List<Urun> Urunler { get; set; }


        public int? KategoriId { get; set; }
    }
}
