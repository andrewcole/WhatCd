using System;
using System.Collections.Generic;
using Illallangi.WhatCD.Model.Artists;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Client
{
    public static class ArtistExtensions
    {
        public static IEnumerable<Artist> GetArtists(this JsonClient client, int id)
        {
            return client.GetArtists(
                new Dictionary<string, string> {{@"id", id.ToString()}},
                $@"%localappdata%\Illallangi Enterprises\WhatCd Client\artistById.{id}.json");
        }

        public static IEnumerable<Artist> GetArtists(this JsonClient client, string name)
        {
            return client.GetArtists(
                new Dictionary<string, string> {{@"artistname", name}},
                $@"%localappdata%\Illallangi Enterprises\WhatCd Client\artist.{name}.json");
        }

        private static IEnumerable<Artist> GetArtists(this JsonClient client, Dictionary<string, string> queryParams,
            string cachePath = null)
        {
            yield return client.GetJsonAction<Artist>(
                @"artist",
                client.GetSessionKey(),
                cachePath: cachePath,
                queryParams: queryParams);
        }
    }
}