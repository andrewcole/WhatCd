using System;

namespace Illallangi
{
    public class JsonResponseStatusException : Exception
    {
        public JsonResponseStatusException(JsonClient jsonClient, string status) : base($"{jsonClient.Uri} returned Error {status}")

        {
            this.Uri = jsonClient.Uri;
            this.Status = status;
        }

        public string Uri { get; }
        public string Status { get; }
    }
}