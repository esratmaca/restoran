using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using rezervasyonAPI.Models;
using rezervasyonAPI.Token;

namespace rezervasyonAPI.Controllers
{
    public class ResarvationController : ApiController
    {

        [HttpGet]
        public IEnumerable<Urunler> GetUrunler()
        {
            using (var db = new RestaurantDBEntities1()) // DbContext adını kendi projenle aynı yap
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.Urunlers.ToList(); // Tüm ürünleri liste olarak döndür
            }
        }

        //ürün ekleme
        [HttpPost]
        [Route("api/resarvation/posturun")]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "admin")]
        public IHttpActionResult PostUrun([FromBody] Urunler urun)
        {
            // Model doğrulaması (örneğin UrunAdi boş bırakılamazsa kontrol eder)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Geçersiz model durumunda 400 Bad Request döndür
            }
            using (var db = new RestaurantDBEntities1()) // DbContext her istek için burada oluşturulur
            {
                try
                {
                    db.Urunlers.Add(urun);
                    db.SaveChanges();

                    // controller adını açıkça belirtin
                    return CreatedAtRoute("DefaultApi", new { controller = "Resarvation"}, urun);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }
        //ürün güncelleme
        [HttpPut]
        [Route("api/reservation/{id}")]//{} içindeki başlıklar opsiyonel demek
        [JwtAuthenticationFilter]
        [Authorize(Roles = "admin")]
        public IHttpActionResult PutUrun(int id, [FromBody] Urunler urun) 
        {
           
            if (id != urun.UrunID)
            {
                return BadRequest("URL'deki ID ile gönderilen ürün ID'si uyuşmuyor.");
            }

            using (var db = new RestaurantDBEntities1()) 
            {
                // Entity'nin durumunu "Modified" olarak işaretler
                // EF'e bu varlığın güncellenmesi gerektiğini söyler
                db.Entry(urun).State = EntityState.Modified;

                try
                {
                    db.SaveChanges(); 
                }
                catch (DbUpdateConcurrencyException) 
                {
                   //ürün yoksa 404 döndür
                    if (!db.Urunlers.Any(e => e.UrunID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; 
                    }
                }
                catch (Exception ex)
                {
                    //diğer hatalar için
                    return InternalServerError(ex);
                }
            }

            // Başarılı güncelleme için 204 No Content döndürür
            return StatusCode(HttpStatusCode.NoContent);
        }

        //ürün silme
        /*postman: https://localhost:44363/api/resarvation/deleteurun/1*/
        [HttpDelete]
        [AllowAnonymous]
        [JwtAuthenticationFilter]
        [Route("api/resarvation/deleteurun/{id}")] 
        [Authorize(Roles = "admin")] //login kısmı yazıldıktan sonra kullanılıcak
        public IHttpActionResult DeleteUrun(int id)
        {
            using (var db = new RestaurantDBEntities1()) 
            {
                var urun = db.Urunlers.Find(id); 
                if (urun == null)
                {
                    return NotFound(); 
                }

                try
                {
                    db.Urunlers.Remove(urun); // Ürünü DbSet'ten kaldır
                    db.SaveChanges(); 
                    return Ok(urun); 
                }
                catch (Exception ex)
                {
                   
                    return InternalServerError(ex);
                }
            }
        }
        [HttpPost]
        [Route("api/account/login")] 
        public IHttpActionResult Login([FromBody] LoginRequest request) // Gelen veriyi LoginRequest modeli olarak al
        {
           
            using (var db = new RestaurantDBEntities1()) 
            {
                // Veritabanından kullanıcıyı sorgula
                var user = db.Kullanicilars
                            .FirstOrDefault(u => u.KullaniciAdi == request.KullaniciAdi && u.Sifre == request.Sifre);

                if (user != null)
                {
                    // JWT'yi JwtHelper sınıfı aracılığıyla oluştur
                    string jwtToken = JwtHelper.GenerateToken(user.KullaniciAdi, user.Rol, user.KullaniciID);

                    // LoginResponse'a JWT token'ını ekle
                    var response = new LoginResponse
                    {
                        KullaniciID = user.KullaniciID,
                        KullaniciAdi = user.KullaniciAdi,
                        Rol = user.Rol,
                        Token = jwtToken // JWT token'ını yanıt olarak gönder
                    };
                    return Ok(response); // 200 OK ve kullanıcı bilgileri + token döndür
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
        [HttpGet]
        [Route("api/account/getmasadurumları")]
        public IHttpActionResult GetMasaDurumları()
        {
            using (var db = new RestaurantDBEntities1()) 
            {
                db.Configuration.ProxyCreationEnabled = false;

                
                var masalarinDurumu = db.Masalars
                                        .Select(m => new MasaDurum 
                                        {
                                            MasaID = m.MasaID,
                                            Durum = m.Durum
                                        })
                                        .ToList();

               

                return Ok(masalarinDurumu);
            }
        }

        private RestaurantDBEntities1 db = new RestaurantDBEntities1();
        [HttpGet]
        [Route("api/resarvation/getsiparislerbymasa/{masaNo}")]
        public IHttpActionResult GetSiparislerByMasa(int masaNo)
        {
            db.Configuration.ProxyCreationEnabled = false; 
            try
            {
                // Masa için "Açık" veya "Ödeme Bekleniyor" durumunda olan ana siparişi bul
                var aktifSiparis = db.Siparislers
                                    .FirstOrDefault(s => s.MasaID == masaNo && (s.OdemeDurumu == "Açık" || s.OdemeDurumu == "Ödeme Bekleniyor"));

                if (aktifSiparis == null)
                {
                    // Masanın aktif (açık/ödeme bekleyen) bir siparişi yoksa boş liste döndür.
                    
                    return Ok(new List<SiparisKalemi>());
                }

                // Aktif siparişe ait tüm detayları (ürünleri) çek ve SiparisKalemi DTO'suna dönüştür
                var siparisDetaylari = db.SiparisDetays
                                         .Where(sd => sd.SiparisID == aktifSiparis.SiparisID)
                                         .Select(sd => new SiparisKalemi // Client'a gönderilecek DTO yapısı
                                         {
                                             SiparisDetayID = sd.DetayID,
                                             SiparisID = sd.SiparisID ?? 0,
                                             UrunID = sd.UrunID ?? 0,
                                             UrunAdi = sd.Urunler.UrunAdi, 
                                             Adet = sd.Adet ?? 0,
                                             Fiyat = sd.BirimFiyat ?? 0m,
                                             MasaNo = masaNo 
                                         })
                                         .ToList();

                return Ok(siparisDetaylari);
            }
            catch (Exception ex)
            {
                // Beklenmedik bir hata olursa sunucu hatası döndür
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("api/resarvation/postsiparisdetay")]
        public IHttpActionResult PostSiparisDetay([FromBody] SiparisDetayDto siparisBilgisi)
        {
            db.Configuration.ProxyCreationEnabled = false; 

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Masa için "Açık" durumda bir ana sipariş var mı diye kontrol et
                var mevcutSiparis = db.Siparislers
                                     .FirstOrDefault(s => s.MasaID == siparisBilgisi.MasaNo && s.OdemeDurumu == "Açık");

                if (mevcutSiparis == null)
                {
                    // Eğer masanın aktif bir siparişi yoksa, yeni bir ana sipariş oluştur 
                    mevcutSiparis = new Siparisler
                    {
                        MasaID = siparisBilgisi.MasaNo,
                        //SiparisTarihi = DateTime.Now, // Siparişin oluşturulma tarihi
                        //OdemeDurumu = "Açık", // Yeni siparişin başlangıç durumu "Açık"
                        //ToplamTutar = 0m // Başlangıçta 0, detaylar eklendikçe güncellenecek
                        //// Diğer zorunlu alanlar (örn. KullaniciID) varsa onları da burada set edin
                    };
                    db.Siparislers.Add(mevcutSiparis);
                    db.SaveChanges(); // Yeni sipariş ID'sini almak için veritabanına kaydet
                }

                // Eklenecek/güncellenecek ürünü veritabanından çek
                var urun = db.Urunlers.Find(siparisBilgisi.UrunID);
                if (urun == null)
                {
                //    return NotFound($"Ürün ID ({siparisBilgisi.UrunID}) bulunamadı."); 
                }

                // Mevcut ana siparişin altında bu ürüne ait bir detay var mı kontrol et
                var siparisDetay = db.SiparisDetays
                                     .FirstOrDefault(sd => sd.SiparisID == mevcutSiparis.SiparisID && sd.UrunID == siparisBilgisi.UrunID);

                if (siparisDetay == null)
                {
                    // Eğer yoksa, yeni bir sipariş detayı ekle
                    siparisDetay = new SiparisDetay
                    {
                        SiparisID = mevcutSiparis.SiparisID,
                        UrunID = siparisBilgisi.UrunID,
                        Adet = siparisBilgisi.Adet,
                        BirimFiyat = urun.UrunFiyat ?? 0m, // Ürünün anlık fiyatını kaydet
                     //   ToplamFiyat = siparisBilgisi.Adet * (urun.UrunFiyat ?? 0m)
                    };
                    db.SiparisDetays.Add(siparisDetay);
                }
                else
                {
                    // Eğer varsa, mevcut sipariş detayının adetini güncelle
                    siparisDetay.Adet += siparisBilgisi.Adet;
                 //   siparisDetay.ToplamFiyat = siparisDetay.Adet * (urun.UrunFiyat ?? 0m); // Toplam fiyatı yeniden hesapla
                    db.Entry(siparisDetay).State = System.Data.Entity.EntityState.Modified; // Değişikliği işaretle
                }

                db.SaveChanges(); // Sipariş detayındaki değişiklikleri kaydet

                // Ana siparişin toplam tutarını tüm detayları üzerinden yeniden hesapla ve güncelle
                //mevcutSiparis.ToplamTutar = db.SiparisDetays
                //                            .Where(sd => sd.SiparisID == mevcutSiparis.SiparisID)
                //                            .Sum(sd => sd.ToplamFiyat);
                //db.Entry(mevcutSiparis).State = System.Data.Entity.EntityState.Modified; // Ana siparişteki değişikliği işaretle
                db.SaveChanges(); 

                return Ok("Sipariş detayı başarıyla eklendi/güncellendi.");
            }
            catch (Exception ex)
            {
                
                return InternalServerError(ex);
            }
        }

        [HttpPost] // Veya [HttpGet] olabilir, duruma göre değişir
        [Route("api/account/logout")] 
        public IHttpActionResult Logout()
        {
            // Web API'de sunucu tarafında oturum yönetimi (Session) olmadığı için,
            // bu metot genellikle client'ın elindeki kimlik doğrulama token'ını silmesi gerektiğini belirtir.
            // Sunucu tarafında özel bir işlem (örneğin token'ı kara listeye alma) yoksa,
            // sadece başarılı yanıt döndürülür.
            return Ok("Başarıyla çıkış yapıldı (client tarafında token temizlenmeli).");
        }
    }

}

