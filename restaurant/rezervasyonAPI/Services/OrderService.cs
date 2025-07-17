using rezervasyonAPI.Models;
using rezervasyonAPI.Services.Results;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace rezervasyonAPI.Services
{
    public class OrderService
    {
        private readonly RestaurantDBEntities1 _dbContext;
        private readonly AccountService _accountService;

        public OrderService(RestaurantDBEntities1 dbContext, AccountService accountService)
        {
            
            _dbContext = dbContext;
            _accountService = accountService;
            _dbContext.Configuration.ProxyCreationEnabled = false;
        }

        public ServiceResult<IEnumerable<SiparisKalemi>> GetSiparislerByMasa(int masaNo)
        {
            try
            {
                var aktifSiparis = _dbContext.Siparislers
                                             .FirstOrDefault(s => s.MasaID == masaNo && (s.OdemeDurumu == "Bekliyor" ));

                List<SiparisKalemi> siparisDetaylari = new List<SiparisKalemi>();

                if (aktifSiparis != null)
                {
                    siparisDetaylari = _dbContext.SiparisDetays
                                               .Where(sd => sd.SiparisID == aktifSiparis.SiparisID)
                                               .Include(sd => sd.Urunler)
                                               .Select(sd => new SiparisKalemi
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
                }

                return ServiceResult<IEnumerable<SiparisKalemi>>.Success(siparisDetaylari, 200);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<SiparisKalemi>>.Fail("Masa siparişleri getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public ServiceResult PostSiparisDetay(SiparisDetayDto siparisBilgisi, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errors = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return ServiceResult.Fail(string.Join("; ", errors), 400);
            }

            try
            {
                var mevcutSiparis = _dbContext.Siparislers
                                             .FirstOrDefault(s => s.MasaID == siparisBilgisi.MasaNo && s.OdemeDurumu == "Bekliyor");

                if (mevcutSiparis == null)
                {
                    mevcutSiparis = new Siparisler
                    {
                        MasaID = siparisBilgisi.MasaNo,
                        TarihSaat = DateTime.Now,
                        OdemeDurumu = "Bekliyor",
                        //ToplamTutar = 0m
                    };
                    _dbContext.Siparislers.Add(mevcutSiparis);
                    _dbContext.SaveChanges();
                    _accountService.UpdateMasaDurumu(siparisBilgisi.MasaNo, "Dolu");
                }

                var urun = _dbContext.Urunlers.Find(siparisBilgisi.UrunID);
                if (urun == null)
                {
                    return ServiceResult.Fail($"Ürün ID ({siparisBilgisi.UrunID}) bulunamadı.", 404);
                }

                var siparisDetay = _dbContext.SiparisDetays
                                            .FirstOrDefault(sd => sd.SiparisID == mevcutSiparis.SiparisID && sd.UrunID == siparisBilgisi.UrunID);

                if (siparisDetay == null)
                {
                    siparisDetay = new SiparisDetay
                    {
                        SiparisID = mevcutSiparis.SiparisID,
                        UrunID = siparisBilgisi.UrunID,
                        Adet = siparisBilgisi.Adet,
                        BirimFiyat = urun.UrunFiyat ?? 0m
                    };
                    _dbContext.SiparisDetays.Add(siparisDetay);
                }
                else
                {
                    if (siparisBilgisi.Adet > 0)
                    {
                        siparisDetay.Adet += siparisBilgisi.Adet;
                        _dbContext.Entry(siparisDetay).State = EntityState.Modified;
                    }
                }

                _dbContext.SaveChanges();

                return ServiceResult.Success(200); // Başarılıysa 200 OK
            }
            catch (Exception ex)
            {
               

                string innerErrorMessage = ex.InnerException != null ? ex.InnerException.Message : "İç hata mesajı yok.";
                System.Diagnostics.Debug.WriteLine($"Sipariş Detay Eklerken Hata: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"İç Hata: {innerErrorMessage}");

                return ServiceResult.Fail($"Sipariş detayı eklenirken/güncellenirken bir hata oluştu: {ex.Message}. Detay: {innerErrorMessage}");

            }
        }

        public ServiceResult CompletePayment(int masaNo)
        {
            try
            {
                var siparis = _dbContext.Siparislers
                    .FirstOrDefault(s => s.MasaID == masaNo &&
                        ( s.OdemeDurumu == "Bekliyor"));

                if (siparis == null)
                    return ServiceResult.Fail("Aktif sipariş bulunamadı.", 404);

                siparis.OdemeDurumu = "Kapalı";
                _dbContext.Entry(siparis).State = EntityState.Modified;

                var updateMasaResult = _accountService.UpdateMasaDurumu(masaNo, "Boş");
                if (!updateMasaResult.IsSuccess)
                {
                    System.Diagnostics.Debug.WriteLine($"Ödeme tamamlanırken masa durumu 'Boş' olarak güncellenemedi: {updateMasaResult.ErrorMessage}");
                    
                }

                _dbContext.SaveChanges(); 

                return ServiceResult.Success(200);
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail("Ödeme sırasında hata oluştu: " + ex.Message);
            }
        }
       


    }
}