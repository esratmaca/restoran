using rezervasyonAPI.Models;
using rezervasyonAPI.Services.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rezervasyonAPI.Services
{
    public class AccountService
    {
        private readonly RestaurantDBEntities1 _dbContext;

        public AccountService(RestaurantDBEntities1 dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Configuration.ProxyCreationEnabled = false;
        }

        public ServiceResult<LoginResponse> Login(LoginRequest request)
        {
            var user = _dbContext.Kullanicilars
                                .FirstOrDefault(u => u.KullaniciAdi == request.KullaniciAdi && u.Sifre == request.Sifre);

            if (user != null)
            {
                string jwtToken = JwtHelper.GenerateToken(user.KullaniciAdi, user.Rol, user.KullaniciID);

                var response = new LoginResponse
                {
                    KullaniciID = user.KullaniciID,
                    KullaniciAdi = user.KullaniciAdi,
                    Rol = user.Rol,
                    Token = jwtToken
                };
                return ServiceResult<LoginResponse>.Success(response, 200); 
            }
            return ServiceResult<LoginResponse>.Fail("Kullanıcı adı veya şifre yanlış.", 401); // 401 Unauthorized
        }

        public ServiceResult UpdateMasaDurumu(int masaID, string yeniDurum)
        {
            var masa = _dbContext.Masalars.Find(masaID);
            if (masa == null)
            {
                return ServiceResult.Fail($"Masa ID ({masaID}) bulunamadı.", 404);
            }

            
            masa.Durum = yeniDurum; // OrderService'den 'Boş' veya 'Dolu' gelecek.
            try
            {
                _dbContext.SaveChanges();
                return ServiceResult.Success(200);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Masa durumu güncellenirken DbUpdate hatası: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"İç hata: {ex.InnerException.Message}");
                }
                return ServiceResult.Fail($"Masa durumu güncellenirken bir hata oluştu: {ex.Message}", 500);
            }
        }

        public ServiceResult<IEnumerable<MasaDurum>> GetMasaDurumlari()
        {
            var masalar = _dbContext.Masalars.ToList();

            var masaDurumlarıListesi = masalar.Select(m => {
                string veritabanindakiDurum = m.Durum ?? "Boş"; 

              

                if (veritabanindakiDurum == "Dolu")
                {
                    var odemeBekleyenSiparis = _dbContext.Siparislers
                        .FirstOrDefault(s => s.MasaID == m.MasaID && s.OdemeDurumu == "Bekliyor");

                    if (odemeBekleyenSiparis != null)
                    {
                        veritabanindakiDurum = "Ödeme Bekleniyor"; // Varsa "Ödeme Bekleniyor" olarak ayarla
                    }
                }

                return new MasaDurum
                {
                    MasaID = m.MasaID,
                    Durum = veritabanindakiDurum // API'den dönecek nihai durum
                };
            }).ToList();

            return ServiceResult<IEnumerable<MasaDurum>>.Success(masaDurumlarıListesi, 200);

        }
    }
}