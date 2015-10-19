using System;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Json
{
    public class EncodingConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return reader.Value.ToString().Replace(@" (VBR)", string.Empty);
            }

            throw new JsonSerializationException($@"Unexpected token type: {reader.TokenType}");
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(string));
        }
    }
}