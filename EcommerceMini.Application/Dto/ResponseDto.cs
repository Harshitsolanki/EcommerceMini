using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceMini.Application.Dto
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseDto() { }

        public ResponseDto(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        // Static methods for creating common response types
        public static ResponseDto<T> SuccessResponse(T data, string message = "Request was successful")
        {
            return new ResponseDto<T>(true, message, data);
        }

        public static ResponseDto<T> ErrorResponse(string message)
        {
            return new ResponseDto<T>(false, message, default(T));
        }
    }
}
