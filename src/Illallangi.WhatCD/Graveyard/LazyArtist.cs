using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Illallangi.WhatCD.Model.Artists;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Graveyard
{
    public class LazyArtist
    {
        public LazyArtist()
        {
            this.Loader = i => { throw new NotImplementedException($@"Did not load Artist ID {i}"); };
        }

        private Artist currentLazy;

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public bool NotificationsEnabled { get { return this.Lazy.NotificationsEnabled; } set { this.Lazy.NotificationsEnabled = value; } }

        [JsonIgnore]
        public bool HasBookmarked { get { return this.Lazy.HasBookmarked; } set { this.Lazy.HasBookmarked = value; } }

        [JsonIgnore]
        public string Image { get { return this.Lazy.Image; } set { this.Lazy.Image = value; } }

        [JsonIgnore]
        public string Body { get { return this.Lazy.Body; } set { this.Lazy.Body = value; } }

        [JsonIgnore]
        public bool VanityHouse { get { return this.Lazy.VanityHouse; } set { this.Lazy.VanityHouse = value; } }

        [JsonIgnore]
        public List<Tag> Tags { get { return this.Lazy.Tags; } set { this.Lazy.Tags = value; } }

        public Artist Lazy
        {
            get { return this.currentLazy ?? (this.currentLazy = this.Loader(this.Id)); }
        }

        public Func<int, Artist> Loader { get; set; }

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