using System.Net;

namespace PhoneBookManagment.BLL.ResponseTypeService
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public T? Value { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        /* Constructor overloading */
        public Response(string? errorMessage)
        {
            Message = errorMessage;
        }

        public Response(string? errorMessage, bool success)
        {
            Message = errorMessage;
            Success = success;
        }

        public Response(string? errorMessage, T? value, bool success, HttpStatusCode statusCode)
        {
            Message = errorMessage;
            Value = value;
            Success = success;
            StatusCode = statusCode;
        }

        public Response(string? errorMessage, HttpStatusCode statusCode)
        {
            Message = errorMessage;
            Value = default;
            Success = false;
            StatusCode = statusCode;
        }

        public static Response<T> Ok(T value)
        {
            return new Response<T>(String.Empty, value, true, HttpStatusCode.OK);
        }

        public static Response<T> NotFound(string errorMessage)
        {
            return new Response<T>(errorMessage, HttpStatusCode.NotFound);
        }

        public static Response<T> SuccessMessage(string message)
        {
            return new Response<T>(message, HttpStatusCode.OK);
        }

        public static Response<T> ExceptionThrow(string ThrowMessage)
        {
            return new Response<T>(ThrowMessage);
        }

        public static Response<T> ErrorMsg(string ThrowMessage)
        {
            return new Response<T>(ThrowMessage);
        }

    }
}
