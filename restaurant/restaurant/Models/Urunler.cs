using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant.Models
{
    public class Urunler
    {
        public int UrunID { get; set; }
        public decimal? UrunFiyat { get; set; }
        public string UrunAdi { get; set; }
        public int? KategoriID { get; set; }
        
    }
}
