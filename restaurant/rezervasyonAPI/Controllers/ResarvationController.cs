using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using rezervasyonAPI.Models;

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
        [Route("api/resarvation/posturun")] // Kullanıcının belirttiği rota
        //[Authorize(Roles = "admin")]
        public IHttpActionResult PostUrun([FromBody] Urunler urun)
        {
            // Model doğrulaması (örneğin UrunAdi boş bırakılamazsa kontrol eder)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Geçersiz model durumunda 400 Bad Request döndür
            }
            //test
            using (var db = new RestaurantDBEntities1()) // DbContext her istek için burada oluşturulur
            {
                try
                {
                    db.Urunlers.Add(urun);
                    db.SaveChanges();

                    // ÖNEMLİ DÜZELTME: controller adını açıkça belirtin
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
        [Route("api/reservation/{id}")]///{} içindeki başlıklar opsiyonel demek
        //[Authorize(Roles = "admin")]
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
        [Route("api/resarvation/deleteurun/{id}")] 
        //[Authorize(Roles = "admin")] //login kısmı yazıldıktan sonra kullanılıcak
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

