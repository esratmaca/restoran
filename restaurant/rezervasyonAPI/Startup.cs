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

            // CORS izinleri - BU KALMALI
            app.UseCors(CorsOptions.AllowAll);

           

            // Bu, HTTP isteklerinin Web API tarafından işlenmesini sağlar - BU KALMALI
            app.UseWebApi(config);
        }

    }
}