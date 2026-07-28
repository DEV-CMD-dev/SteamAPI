using System.Net;

namespace BusinessLogic
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public HttpException(
            string message,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpException(
            string message,
            Exception innerException,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}