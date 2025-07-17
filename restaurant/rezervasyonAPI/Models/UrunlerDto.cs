using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rezervasyonAPI.Models
{
    public class UrunlerDto
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public decimal? UrunFiyat { get; set; }
        public int? KategoriID {  get; set; } 
      
    }
}