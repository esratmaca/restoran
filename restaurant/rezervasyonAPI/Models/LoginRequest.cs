using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rezervasyonAPI.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz.")] // Bu alanın zorunlu olduğunu belirtir
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre boş olamaz.")] // Bu alanın zorunlu olduğunu belirtir
        public string Sifre { get; set; }
    }
}