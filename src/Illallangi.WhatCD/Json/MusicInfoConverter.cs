using System;
using System.Collections.Generic;
using System.Linq;
using Illallangi.WhatCD.Model;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Json
{
    public class MusicInfoConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var musicInfo = serializer.Deserialize<MusicInfo>(reader);
            return null == musicInfo ?
                new List<int>() :
                musicInfo.ToList();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        private class MusicInfo
        {
            [JsonProperty("composers")]
            public List<ObjectWithId> Composers { get; set; }

            [JsonProperty("dj")]
            public List<ObjectWithId> Dj { get; set; }

            [JsonProperty("artists")]
            public List<ObjectWithId> Artists { get; set; }

            [JsonProperty("with")]
            public List<ObjectWithId> With { get; set; }

            [JsonProperty("conductor")]
            public List<ObjectWithId> Conductor { get; set; }

            [JsonProperty("remixedBy")]
            public List<ObjectWithId> RemixedBy { get; set; }

            [JsonProperty("producer")]
            public List<ObjectWithId> Producer { get; set; }

            public List<int> ToList()
            {
                var result = new List<int>();
                foreach (
                    var artistList in
                        new[]
                        {
                            this.Artists, this.Composers, this.Dj, this.With, this.Conductor, this.RemixedBy, this.Producer
                        })
                {
                    result.AddRange(artistList.Select(i => i.Id));
                }
                return result;
            }
        }
    }
}