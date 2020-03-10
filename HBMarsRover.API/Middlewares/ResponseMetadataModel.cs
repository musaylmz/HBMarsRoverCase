using System.Net;

namespace HBMarsRover.API.Middlewares
{
    public class ResponseMetadataModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
        public object Result { get; set; }
        public ResponseMetadataModel()
        {
        }
        public ResponseMetadataModel(HttpStatusCode statusCode, object result = null, string error = null)
        {
            StatusCode = statusCode;
            Result = result;
            Error = error;
            Success = true;
        }
        public static ResponseMetadataModel Create(HttpStatusCode statusCode, object result = null, string error = null)
        {
            return new ResponseMetadataModel(
                statusCode: statusCode,
                result: result,
                error: error);
        }
    }
}
