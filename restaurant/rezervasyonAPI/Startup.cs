using System; 
using System.Web.Http; 
using System.Text; 
using Owin;
using Microsoft.Owin; // OwinStartup ve IAppBuilder için
using Microsoft.Owin.Cors; 
using Microsoft.Owin.Security; // AuthenticationMode için
using Microsoft.Owin.Security.Jwt; 
using Microsoft.IdentityModel.Tokens;

// Bu OWIN'e uygulamanın başlangıç sınıfını bildirir
// OWIN, uygulamayı başlatırken bu sınıfın Configuration metodunu arar
[assembly: OwinStartup(typeof(rezervasyonAPI.Startup))]

namespace rezervasyonAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Web API routing
            WebApiConfig.Register(config);

            // JWT ayarları
            var key = Encoding.UTF8.GetBytes("Secretkey123@");

            app.UseCors(CorsOptions.AllowAll); // CORS izinleri

            // JWT Bearer Kimlik Doğrulama Middleware'ini etkinleştirir.
            // Bu middleware, gelen HTTP isteklerinin Authorization başlığındaki JWT'yi doğrulayacaktır.
            
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                // Kimlik doğrulama modunu aktif olarak ayarlar.
                // Bu, middleware'in her gelen isteği kimlik doğrulamasından geçirmesi gerektiğini belirtir.
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    // ValidateIssuer = false: Token'ı veren (issuer) bilginin doğrulanmayacağını belirtir.
                    ValidateIssuer = false,
                    // ValidateAudience = false: Token'ın hedef kitlesi (audience) bilginin doğrulanmayacağını belirtir.
                    ValidateAudience = false,
                    // ValidateIssuerSigningKey = true: Token'ın imzalayan anahtarının doğrulanacağını belirtir.
                    // Bu, token'ın değiştirilmediğinden veya sahte olmadığından emin olmak için kritik öneme sahiptir.
                    ValidateIssuerSigningKey = true,
                    // IssuerSigningKey: Token'ı doğrulamak için kullanılacak gizli anahtarı sağlar.
                    // Bu, JwtHelper'da token'ı imzalamak için kullanılan anahtarın aynısı olmalıdır.
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true
                }
            });

            // Bu, HTTP isteklerinin Web API tarafından işlenmesini sağlar
            app.UseWebApi(config);
        }

    }
}