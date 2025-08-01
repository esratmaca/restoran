﻿using rezervasyonAPI.Controllers;
using rezervasyonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace rezervasyonAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API yapılandırması ve hizmetler

            // Web API yolları
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //apı debug oluşturma
            //SiparisDetayDto sd = new SiparisDetayDto
            //{
            //    MasaNo = 3,
            //    UrunID = 16,
            //    Adet = 10
            //};
            var esra = new ResarvationController().GetSiparislerByMasa(7);
        }
    }
}
