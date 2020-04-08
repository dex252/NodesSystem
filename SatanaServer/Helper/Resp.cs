using Nancy;

namespace SatanaServer.Helper
{
    internal static class Resp
    {
        public enum ContentType
        {
            json,
            zip,
            xml,
            mpeg,
            jpeg,
            png,
            text
        }

        /// <summary>
        ///    Возвращает ответ с HttpStatusCode.OK
        /// </summary>
        public static Nancy.Response SendResponse()
        {
            return new Nancy.Response()
            {
                StatusCode = HttpStatusCode.OK
            };
        }

        /// <summary>
        ///    Возвращает ответ с указанным кодом ошибки
        /// </summary>
        public static Nancy.Response SendResponse(HttpStatusCode statusCode)
        {
            return new Nancy.Response()
            {
                StatusCode = statusCode
            };
        }

        /// <summary>
        ///    Возвращает ответ с  HttpStatusCode.OK, типа application/json, в качестве тела запроса принимается массив байт
        /// </summary>
        public static Nancy.Response SendResponse(byte[] jsonBytes)
        {
            return new Nancy.Response()
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = ContentTypeToString(ContentType.json),
                Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
            };
        }
        /// <summary>
        ///    Возвращает ответ с указанным кодом ошибки, типа application/json, в качестве тела запроса принимается массив байт
        /// </summary>
        public static Nancy.Response SendResponse(HttpStatusCode statusCode, byte[] jsonBytes)
        {
            return new Nancy.Response()
            {
                StatusCode = statusCode,
                ContentType = ContentTypeToString(ContentType.json),
                Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
            };
        }

        /// <summary>
        ///    Возвращает ответ с указанным кодом ошибки, типом содержимого, в качестве тела запроса принимается массив байт
        /// </summary>
        public static Nancy.Response SendResponse(HttpStatusCode statusCode, ContentType contentType, byte[] jsonBytes)
        {
            return new Nancy.Response()
            {
                StatusCode = statusCode,
                ContentType = ContentTypeToString(contentType),
                Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
            };
        }

        private static string ContentTypeToString(ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.json:
                    {
                        return "application/json";
                    }

                case ContentType.zip:
                    {
                        return "application/zip";
                    }

                case ContentType.xml:
                    {
                        return "application/xml";
                    }

                case ContentType.mpeg:
                    {
                        return "audio/mpeg";
                    }

                case ContentType.jpeg:
                    {
                        return "image/jpeg";
                    }

                case ContentType.png:
                    {
                        return "image/png";
                    }

                case ContentType.text:
                    {
                        return "text/html; charset=utf-8";
                    }
                default:
                    {
                        return "application/json";
                    }
            }
        }
    }
}
