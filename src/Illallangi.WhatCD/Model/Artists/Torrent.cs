using Newtonsoft.Json;

namespace Illallangi.WhatCD.Model.Artists
{
    public class Torrent
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("groupId")]
        public int GroupId { get; set; }
        [JsonProperty("media")]
        public string Media { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("encoding")]
        public string Encoding { get; set; }
        [JsonProperty("remasterYear")]
        public int RemasterYear { get; set; }
        [JsonProperty("remastered")]
        public bool Remastered { get; set; }
        [JsonProperty("remasterTitle")]
        public string RemasterTitle { get; set; }
        [JsonProperty("remasterRecordLabel")]
        public string RemasterRecordLabel { get; set; }
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
        [JsonProperty("freeTorrent")]
        public bool FreeTorrent { get; set; }
        [JsonProperty("size")]
        public long Size { get; set; }
        [JsonProperty("leechers")]
        public int Leechers { get; set; }
        [JsonProperty("seeders")]
        public int Seeders { get; set; }
        [JsonProperty("snatched")]
        public int Snatched { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("hasFile")]
        public int HasFile { get; set; }
    }
}