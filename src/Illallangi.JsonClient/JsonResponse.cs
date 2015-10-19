using Newtonsoft.Json;

namespace Illallangi
{
    public class JsonResponse<T>
    {
        [JsonProperty(@"status")]
        public string Status { get; set; }

        [JsonProperty(@"response")]
        public T Response { get; set; }
    }

}
