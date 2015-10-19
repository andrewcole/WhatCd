using System.Collections.Generic;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Graveyard
{
    public class MusicInfo
    {
        [JsonProperty("composers")]
        public List<LazyArtist> Composers { get; set; }

        [JsonProperty("dj")]
        public List<LazyArtist> Dj { get; set; }

        [JsonProperty("artists")]
        public List<LazyArtist> Artists { get; set; }

        [JsonProperty("with")]
        public List<LazyArtist> With { get; set; }

        [JsonProperty("conductor")]
        public List<LazyArtist> Conductor { get; set; }

        [JsonProperty("remixedBy")]
        public List<LazyArtist> RemixedBy { get; set; }

        [JsonProperty("producer")]
        public List<LazyArtist> Producer { get; set; }
    }
}