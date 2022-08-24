using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharedLibrary.DTOs
{
    public class ResponseDto<T> where T : class
    {
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode, isSuccessful = true };
        }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default, StatusCode = statusCode, isSuccessful = true };
        }

        public static ResponseDto<T> Fail(ErrorDto errorDto, int statusCode)
        {
            return new ResponseDto<T> { Error = errorDto, StatusCode = statusCode, isSuccessful = false };
        }

        public static ResponseDto<T> Fail(string errorMessage, int statusCode, bool isShow)
        {
            return new ResponseDto<T> { Error = new ErrorDto(errorMessage, isShow), StatusCode = statusCode, isSuccessful = false };
        }

        [JsonProperty("data")]
        public T Data { get; private set; }
        [JsonProperty("statusCode")]
        public int StatusCode { get; private set; }
        [JsonProperty("error")]
        public ErrorDto Error { get; private set; }
        [JsonIgnore]
        public bool isSuccessful { get; private set; }
    }
}
