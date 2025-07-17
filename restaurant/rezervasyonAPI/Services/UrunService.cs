// Örnek: UrunService.cs (Sadece PostUrun metodunu gösteriyorum, diğerleri de benzer mantıkla güncellenmeli)
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http.ModelBinding; // ModelStateDictionary için
using rezervasyonAPI.Models;
using rezervasyonAPI.Services.Results; // Yeni sonuç sınıflarımızın bulunduğu yer

namespace rezervasyonAPI.Services
{
    public class UrunService
    {
        private readonly RestaurantDBEntities1 _dbContext;

        public UrunService(RestaurantDBEntities1 dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Configuration.ProxyCreationEnabled = false;
        }

        // GET işlemi hala IEnumerable<Urunler> döndürebilir, çünkü bir hata durumu yoksa basit veri döndürür.
        public IEnumerable<Urunler> GetAllUrunler()
        {
            return _dbContext.Urunlers.ToList();
        }

        public ServiceResult<Urunler> AddUrun(Urunler urun, ModelStateDictionary modelState)
        {
            
            if (!modelState.IsValid)
            {
                
                var errors = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return ServiceResult<Urunler>.Fail(string.Join("; ", errors), 400); // 400 Bad Request için
            }

            try
            {
                _dbContext.Urunlers.Add(urun);
                _dbContext.SaveChanges();
                return ServiceResult<Urunler>.Success(urun, 201); // 201 Created için
            }
            catch (Exception ex)
            {
                return ServiceResult<Urunler>.Fail("Ürün eklenirken bir hata oluştu: " + ex.Message); // 500 Internal Server Error için
            }
        }

        public ServiceResult UpdateUrun(int id, Urunler urun) // Dönecek bir data olmadığı için non-generic ServiceResult
        {
            if (id != urun.UrunID)
            {
                return ServiceResult.Fail("URL'deki ID ile gönderilen ürün ID'si uyuşmuyor.", 400); // 400 Bad Request
            }

            try
            {
                _dbContext.Entry(urun).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return ServiceResult.Success(204); // 204 No Content
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Urunlers.Any(e => e.UrunID == id))
                {
                    return ServiceResult.Fail("Güncellenmek istenen ürün bulunamadı.", 404); // 404 Not Found
                }
                else
                {
                    // Diğer eşzamanlılık hataları için daha genel bir hata
                    return ServiceResult.Fail("Ürün güncellenirken bir eşzamanlılık hatası oluştu.", 409); // 409 Conflict
                }
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail("Ürün güncellenirken bir hata oluştu: " + ex.Message); // 500 Internal Server Error
            }
        }

        public ServiceResult DeleteUrun(int id)
        {
            try
            {
                var urun = _dbContext.Urunlers.Find(id);
                if (urun == null)
                {
                    return ServiceResult.Fail("Silinmek istenen ürün bulunamadı.", 404); // 404 Not Found
                }

                _dbContext.Urunlers.Remove(urun);
                _dbContext.SaveChanges();
                return ServiceResult.Success(200); // 200 OK
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail("Ürün silinirken bir hata oluştu: " + ex.Message); // 500 Internal Server Error
            }
        }
    }
}