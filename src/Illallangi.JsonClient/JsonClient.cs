using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

namespace Illallangi
{
    public sealed class JsonClient
    {
        public JsonClient(string uri)
        {
            this.Uri = uri;
            this.FlurlClient = new FlurlClient().EnableCookies();
        }

        public Dictionary<string, Tuple<string, DateTime>> PostCookieAction(
            string path = @"/login.php",
            ICollection<KeyValuePair<string, string>> queryParams = null,
            ICollection<KeyValuePair<string, string>> cookies = null,
            ICollection<KeyValuePair<string, string>> postParams = null)
        {
            postParams = postParams ?? new Dictionary<string, string>();
            cookies = cookies ?? new Dictionary<string, string>();

            var result = new Url(this.Uri)
                .AppendPathSegment(path)
                .SetQueryParams(queryParams)
                .WithClient(this.FlurlClient)
                .WithCookies(cookies)
                .PostUrlEncodedAsync(postParams)
                .Result;

            var dictionary = this.FlurlClient.GetCookies();
            return dictionary.ToDictionary(cookie => cookie.Key,
                cookie => new Tuple<string, DateTime>(cookie.Value.Value, cookie.Value.Expires));
        }

        public T GetJsonAction<T>(string action, 
            string session,
            string path = @"/ajax.php", 
            string cachePath = null,
            ICollection<KeyValuePair<string, string>> queryParams = null)
        {
            var expandEnvironmentVariables = Environment.ExpandEnvironmentVariables(cachePath ?? string.Empty);

            if (!string.IsNullOrWhiteSpace(cachePath) && File.Exists(expandEnvironmentVariables))
            {
                return
                    JsonConvert.DeserializeObject<JsonResponse<T>>(File.ReadAllText(expandEnvironmentVariables))
                        .Response;
            }

            queryParams = queryParams ?? new Dictionary<string, string>();
            
            if (!string.IsNullOrWhiteSpace(action))
            {
                queryParams.Add(new KeyValuePair<string, string>(@"action", action));
            }

            this.OnQuerying();

            var httpResponse = new Url(this.Uri)
                .AppendPathSegment(path)
                .SetQueryParams(queryParams)
                .WithClient(this.FlurlClient)
                .WithCookie(@"session", session, DateTime.Now.AddHours(1))
                .GetAsync()
                .Result;

            var json = httpResponse.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<JsonResponse<T>>(json);
            
            this.OnQueried();

            if (@"success" != result.Status)
            {
                throw new JsonResponseStatusException(this, result.Status);
            }

            if (!string.IsNullOrWhiteSpace(expandEnvironmentVariables))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(expandEnvironmentVariables));
                File.WriteAllText(expandEnvironmentVariables, json);
            }

            return result.Response;
        }

        public event EventHandler Querying;

        public event EventHandler Queried;

        private void OnQuerying()
        {
            this.Querying?.Invoke(this, EventArgs.Empty);
        }

        private void OnQueried()
        {
            this.Queried?.Invoke(this, EventArgs.Empty);
        }

        public string Uri { get; }

        private FlurlClient FlurlClient { get; }
    }
}