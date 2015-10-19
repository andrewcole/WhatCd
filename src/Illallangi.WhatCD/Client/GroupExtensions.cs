using System.Collections.Generic;
using System.Linq;
using Illallangi.WhatCD.Model;
using Illallangi.WhatCD.Model.Groups;
using Illallangi.WhatCD.Model.Torrents;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Client
{
    public static class GroupExtensions
    {
        public static IEnumerable<Group> GetGroups(this JsonClient client, int id)
        {
            return client.GetGroups(
                new Dictionary<string, string> {{@"id", id.ToString()}},
                $@"%localappdata%\Illallangi Enterprises\WhatCd Client\groupById.{id}.json");
        }

        public static IEnumerable<Group> GetGroups(this JsonClient client, string hash)
        {
            return client.GetGroups(
                new Dictionary<string, string> {{@"hash", hash.ToUpperInvariant()}},
                $@"%localappdata%\Illallangi Enterprises\WhatCd Client\group.{hash}.json");
        }

        private static IEnumerable<Group> GetGroups(this JsonClient client, Dictionary<string, string> queryParams, string cachePath = null)
        {
            return client.GetResponse(
                queryParams, 
                cachePath).Select(
                    r =>
                    {
                        var result = r.Group;
                        result.TorrentIds = r.Torrents.Select(t => t.Id).ToList();
                        if (result.Category.Equals(@"Music"))
                        {
                            var genre = client.GetArtists(result.ArtistIds.First()).First().Genre;
                            if (genre.Equals(@"Musical") || genre.Equals(@"Score"))
                            {
                                result.ReleaseType = ReleaseType.Soundtrack;
                            }
                        }
                        return result;
                    });
        }

        private static IEnumerable<GroupResponse> GetResponse(this JsonClient client, Dictionary<string, string> queryParams, string cachePath = null)
        {
            yield return client.GetJsonAction<GroupResponse>(
                @"torrentgroup",
                client.GetSessionKey(),
                cachePath: cachePath,
                queryParams: queryParams);
        }

        private class GroupResponse
        {
            [JsonProperty(@"group")]
            public Group Group { get; set; }

            [JsonProperty(@"torrents")]
            public List<ObjectWithId> Torrents { get; set; }
        }
    }
}