using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant
{
    //WinFormda kullanıcı oturum bilgilerini (özellikle JWT token'ını ve kullanıcının rolünü) uygulamanın farklı formları
    //ve bileşenleri arasında kolayca ve merkezi bir şekilde erişilebilir kılmak
    public static class TokenStorage
    {
        public static string JwtToken { get; set; }
        public static int KullaniciID { get; set; }
        public static string KullaniciAdi { get; set; }
        public static string Rol { get; set; }
    }

}
