using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rezervasyonAPI.Models
{
    public class SiparisDetayDto
    {
        public int MasaNo { get; set; }
        public int UrunID { get; set; }
        public int Adet { get; set; }
    }
}