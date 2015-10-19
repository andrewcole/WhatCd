using System;
using System.Net;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Json
{
    public class HtmlEntityConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return WebUtility.HtmlDecode(reader.Value.ToString().Replace(@"&Prime;", @"&quot;"));
            }

            throw new JsonSerializationException($@"Unexpected token type: {reader.TokenType}");
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof (string));
        }
    }
}