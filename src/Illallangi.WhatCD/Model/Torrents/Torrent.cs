using System.Net;
using System.Text;
using Illallangi.WhatCD.Json;
using Illallangi.WhatCD.Model.Groups;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Model.Torrents
{
    public class Torrent
    {
        [JsonProperty("id")]
        public int TorrentId { get; set; }

        [JsonProperty("infoHash")]
        public string Hash { get; set; }

        [JsonIgnore]
        public int GroupId { get; set; }

        [JsonProperty("media")]
        public string Media { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonConverter(typeof(EncodingConverter))]
        [JsonProperty("encoding")]
        public string Encoding { get; set; }

        [JsonProperty("remastered")]
        public bool Remastered { get; set; }

        [JsonProperty("remasterYear")]
        public int RemasterYear { get; set; }

        [JsonConverter(typeof(HtmlEntityConverter))]
        [JsonProperty("remasterTitle")]
        public string RemasterTitle { get; set; }

        [JsonProperty("remasterRecordLabel")]
        public string RemasterLabel { get; set; }

        [JsonProperty("remasterCatalogueNumber")]
        public string RemasterCatalogueNumber { get; set; }

        [JsonProperty("scene")]
        public bool Scene { get; set; }

        [JsonProperty("hasLog")]
        public bool HasLog { get; set; }

        [JsonProperty("hasCue")]
        public bool HasCue { get; set; }

        [JsonProperty("logScore")]
        public int LogScore { get; set; }

        [JsonProperty("fileCount")]
        public int FileCount { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("seeders")]
        public int Seeders { get; set; }

        [JsonProperty("leechers")]
        public int Leechers { get; set; }

        [JsonProperty("snatched")]
        public int Snatched { get; set; }

        [JsonProperty("freeTorrent")]
        public bool FreeTorrent { get; set; }

        [JsonProperty("reported")]
        public bool Reported { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fileList")]
        public string FileList { get; set; }

        [JsonConverter(typeof(HtmlEntityConverter))]
        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonIgnore]
        public string Title { get; set; }
    }
}