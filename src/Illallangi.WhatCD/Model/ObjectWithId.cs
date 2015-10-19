using Newtonsoft.Json;

namespace Illallangi.WhatCD.Model
{
    public class ObjectWithId
    {
        [JsonProperty(@"id")]
        public int Id { get; set; }
    }
}