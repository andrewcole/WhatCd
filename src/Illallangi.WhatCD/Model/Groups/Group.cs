using System.Collections.Generic;
using Illallangi.WhatCD.Json;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Model.Groups
{
    public class Group
    {
        [JsonProperty("id")]
        public int GroupId { get; set; }

        [JsonConverter(typeof(HtmlEntityConverter))]
        [JsonProperty(@"name")]
        public string Name { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("wikiBody")]
        public string WikiBody { get; set; }

        [JsonProperty("wikiImage")]
        public string WikiImage { get; set; }
        
        [JsonProperty("recordLabel")]
        public string Label { get; set; }

        [JsonProperty("catalogueNumber")]
        public string CatalogueNumber { get; set; }
        
        [JsonProperty(@"releaseType")]
        public ReleaseType ReleaseType { get; set; }
        
        //[JsonProperty("categoryId")]
        //public int CategoryId { get; set; }

        [JsonProperty("categoryName")]
        public string Category { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("vanityHouse")]
        public bool VanityHouse { get; set; }

        //[JsonProperty("isBookmarked")]
        //public bool IsBookmarked { get; set; }

        //[JsonProperty("musicInfo")]
        //public MusicInfo MusicInfo { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonIgnore]
        public List<int> TorrentIds { get; set; } 

        [JsonProperty(@"musicInfo")]
        [JsonConverter(typeof(MusicInfoConverter))]
        public List<int> ArtistIds { get; set; } 
    }
}