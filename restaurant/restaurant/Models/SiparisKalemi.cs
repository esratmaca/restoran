using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant.Models
{
    public class SiparisKalemi 
    {
        public int SiparisDetayID { get; set; }
        public int SiparisID { get; set; }
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamFiyat => Adet * Fiyat;
        public int MasaNo { get; set; }
    }
}
