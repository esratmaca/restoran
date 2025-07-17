using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rezervasyonAPI.Services.Results
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        // Parametresiz Success metodu
        public static ServiceResult Success()
        {
            return new ServiceResult { IsSuccess = true, StatusCode = 200 };
        }

        // Tek int parametreli Success metodu (belirli bir durum kodu için)
        public static ServiceResult Success(int statusCode)
        {
            return new ServiceResult { IsSuccess = true, StatusCode = statusCode };
        }

        // Hata durumu için Fail metodu
        public static ServiceResult Fail(string errorMessage, int statusCode = 500)
        {
            return new ServiceResult { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = statusCode };
        }
    }

    // Belirli bir veri tipi döndüren işlemler için generic sonuç sınıfı
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T> { IsSuccess = true, Data = data, StatusCode = 200 };
        }

        // Veri ve int parametreli Success metodu (belirli bir durum kodu için)
        public static ServiceResult<T> Success(T data, int statusCode)
        {
            return new ServiceResult<T> { IsSuccess = true, Data = data, StatusCode = statusCode };
        }

        // Hata durumu için Fail metodu
        // Base class'taki Fail metodu ile imzası birebir aynı olduğu için new kullanıldı
        public new static ServiceResult<T> Fail(string errorMessage, int statusCode = 500)
        {
            return new ServiceResult<T> { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = statusCode };
        }
    }
}