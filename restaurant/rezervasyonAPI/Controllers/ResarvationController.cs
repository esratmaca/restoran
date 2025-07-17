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
using rezervasyonAPI.Services;


namespace rezervasyonAPI.Controllers
{
    [RoutePrefix("api/resarvation")]
    public class ResarvationController : ApiController
    {

        private readonly UrunService _urunService;
        private readonly AccountService _accountService;
        private readonly OrderService _orderService;

        public ResarvationController()
        {
            var dbContext = new RestaurantDBEntities1();
            _urunService = new UrunService(dbContext);
            _accountService = new AccountService(dbContext);
            _orderService = new OrderService(dbContext, _accountService);
        }

         
        [HttpGet]
        [Route("geturunler")]
        public IEnumerable<Urunler> GetUrunler()
        {
            return _urunService.GetAllUrunler();
        }

        [HttpPost]
        [Route("posturun")]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "admin")]
        public IHttpActionResult PostUrun([FromBody] Urunler urun)
        {
            var result = _urunService.AddUrun(urun, ModelState);
            return result.ToHttpResult(Request);
        }

        [HttpPut]
        [Route("puturun/{id}")]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "admin")]
        public IHttpActionResult PutUrun(int id, [FromBody] Urunler urun)
        {
            var result = _urunService.UpdateUrun(id, urun);

            if (result.IsSuccess)
            {
                return StatusCode((HttpStatusCode)result.StatusCode); // 204 No Content
            }
            else
            {
                return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage); 
            }
        }

        [HttpDelete]
        [Route("deleteurun/{id}")]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "admin")]
        public IHttpActionResult DeleteUrun(int id)
        {
            var result = _urunService.DeleteUrun(id);

            if (result.IsSuccess)
            {
                return StatusCode((HttpStatusCode)result.StatusCode);
            }
            else
            {
                return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage); 
            }
        }

        // Hesap/Yetkilendirme İşlemleri
        [HttpPost]
        [Route("~/api/account/login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            var result = _accountService.Login(request);

            if (result.IsSuccess)
            {
                return Content((HttpStatusCode)result.StatusCode, result.Data); // 200 OK ve LoginResponse
            }
            else
            {
                return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage); // 401 Unauthorized
            }
        }

        [HttpGet]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "garson")]
        [Route("~/api/account/getmasadurumlari")]
        public IHttpActionResult GetMasaDurumlari()
        {
            var result = _accountService.GetMasaDurumlari();

            if (result.IsSuccess)
            {
                return Content((HttpStatusCode)result.StatusCode, result.Data); // 200 OK ve MasaDurumları listesi
            }
            else
            {
                return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage); // Genelde burada hata beklenmez ama yine de
            }
        }

        [HttpPut]
        [Route("~/api/account/updatemasadurumu")]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "garson")] 
        public IHttpActionResult UpdateMasaDurumu([FromBody] MasaDurumDto dto)
        {
            var result = _accountService.UpdateMasaDurumu(dto.MasaID, dto.Durum);
            if (result.IsSuccess)
            {
                return StatusCode((HttpStatusCode)result.StatusCode);
            }
            else
            {
                return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage);
            }
        }


        [HttpPost]
        [Route("account/logout")]
        public IHttpActionResult Logout()
        {
            return Ok("Başarıyla çıkış yapıldı (client tarafında token temizlenmeli).");
        }

        // Sipariş İşlemleri
        [HttpGet]
        [Route("getsiparislerbymasa/{masaID}")]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "garson")]
        public IHttpActionResult GetSiparislerByMasa(int masaID)
        {
            var result = _orderService.GetSiparislerByMasa(masaID);

            if (result.IsSuccess)
            {
                return Content((HttpStatusCode)result.StatusCode, result);
            }
            else
            {
                return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage); // 500 Internal Server Error
            }
        }

        [HttpPost]
        [Route("postsiparisdetay")]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "garson")]
        public IHttpActionResult PostSiparisDetay([FromBody] SiparisDetayDto siparisBilgisi)
        {
            var result = _orderService.PostSiparisDetay(siparisBilgisi, ModelState);

            if (result.IsSuccess)
            {
                return Content((HttpStatusCode)result.StatusCode, "Sipariş detayı başarıyla eklendi/güncellendi."); // 200 OK
            }
            else
            {
                return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage); 
            }
        }

        [HttpPost]
        [Route("odemeal/{masaNo}")]
        [JwtAuthenticationFilter]
        [Authorize(Roles = "garson")]
        public IHttpActionResult OdemeAl(int masaNo)
        {
            var result = _orderService.CompletePayment(masaNo);

            if (result.IsSuccess)
                return StatusCode((HttpStatusCode)result.StatusCode);

            return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage);
        }

        [HttpPost]
        [Route("odemetamamla/{masaID}")]
        public IHttpActionResult OdemeTamamla(int masaID)
        {
            var result = _orderService.CompletePayment(masaID);

            if (result.IsSuccess)
            {
                return StatusCode((HttpStatusCode)result.StatusCode);
            }
            else
            {
                return Content((HttpStatusCode)result.StatusCode, result.ErrorMessage);
            }
        }


    }

}

