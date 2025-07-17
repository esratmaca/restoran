using rezervasyonAPI.Services.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace rezervasyonAPI.Services
{
    public static class ServiceResultExtensions
    {
        // Generic versiyon
        public static IHttpActionResult ToHttpResult<T>(this ServiceResult<T> result, HttpRequestMessage request)
        {
            if (result.IsSuccess)
            {
                var response = request.CreateResponse((HttpStatusCode)result.StatusCode, result.Data);
                return new ResponseMessageResult(response);
            }
            else
            {
                var response = request.CreateErrorResponse((HttpStatusCode)result.StatusCode, result.ErrorMessage ?? "Bilinmeyen hata");
                return new ResponseMessageResult(response);
            }
        }

        // Non-generic versiyon
        public static IHttpActionResult ToHttpResult(this ServiceResult result, HttpRequestMessage request)
        {
            if (result.IsSuccess)
            {
                var response = request.CreateResponse((HttpStatusCode)result.StatusCode);
                return new ResponseMessageResult(response);
            }
            else
            {
                var response = request.CreateErrorResponse((HttpStatusCode)result.StatusCode, result.ErrorMessage ?? "Bilinmeyen hata");
                return new ResponseMessageResult(response);
            }
        }
    }
}