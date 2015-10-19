using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Model.Artists
{
    public class Artist
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notificationsEnabled")]
        public bool NotificationsEnabled { get; set; }

        [JsonProperty("hasBookmarked")]
        public bool HasBookmarked { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("vanityHouse")]
        public bool VanityHouse { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("similarArtists")]
        public List<SimilarArtist> SimilarArtists { get; set; }

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }

        [JsonProperty("torrentgroup")]
        public List<Torrentgroup> Torrentgroup { get; set; }

        [JsonProperty("requests")]
        public List<Request> Requests { get; set; }

        public string GetGenre()
        {
            return
               CultureInfo.InvariantCulture.TextInfo.ToTitleCase(
                   this.Tags.Aggregate((i1, i2) => i1.count > i2.count ? i1 : i2).name).Replace(@".", @" ");

        }


        [JsonIgnore]
        public string Genre
        {
            get
            {
                var artistGenreCollection = ArtistGenreCollection.Load();

                var result = artistGenreCollection.SingleOrDefault(i => i.Key.Equals(this.Id));

                return default(KeyValuePair<int, string>).Equals(result)
                    ? this.GetGenre()
                    : result.Value;
            }

            set
            {
                var artistGenreCollection = ArtistGenreCollection.Load();

                var result = artistGenreCollection.SingleOrDefault(i => i.Key.Equals(this.Id));

                if (!default(KeyValuePair<int, string>).Equals(result))
                {
                    artistGenreCollection.Remove(result);
                }

                if (value != this.GetGenre())
                {
                    artistGenreCollection.Add(new KeyValuePair<int, string>(
                        this.Id,
                        value));
                }
            }
        }
    }
}