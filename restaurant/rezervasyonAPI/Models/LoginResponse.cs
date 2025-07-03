using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rezervasyonAPI.Models
{
    public class LoginResponse
    {
     
        public int KullaniciID { get; set; }

        public string KullaniciAdi { get; set; }
        public string Rol { get; set; }

        public string Token { get; set; }
        
    }
}