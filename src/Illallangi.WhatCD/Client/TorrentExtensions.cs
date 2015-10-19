using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illallangi.WhatCD.Model;
using Illallangi.WhatCD.Model.Artists;
using Illallangi.WhatCD.Model.Groups;
using Newtonsoft.Json;
using Torrent = Illallangi.WhatCD.Model.Torrents.Torrent;

namespace Illallangi.WhatCD.Client
{
    public static class TorrentExtensions
    {
        public static IEnumerable<Torrent> GetTorrents(this JsonClient client, int id)
        {
            return client.GetTorrents(
                new Dictionary<string, string> {{@"id", id.ToString()}},
                $@"%localappdata%\Illallangi Enterprises\WhatCd Client\torrentById.{id}.json");
        }

        public static IEnumerable<Torrent> GetTorrents(this JsonClient client, string hash)
        {
            return client.GetTorrents(
                new Dictionary<string, string> {{@"hash", hash.ToUpperInvariant()}},
                $@"%localappdata%\Illallangi Enterprises\WhatCd Client\torrent.{hash}.json");
        }

        private static IEnumerable<Torrent> GetTorrents(this JsonClient client, Dictionary<string, string> queryParams,
            string cachePath = null)
        {
            return client.GetResponse(
                queryParams,
                cachePath).Select(
                    r =>
                    {
                        var result = r.Torrent;
                        result.GroupId = r.Group.Id;
                        result.Title = r.Torrent.GetTitle(client);
                        return result;
                    });
        }

        private static IEnumerable<TorrentResponse> GetResponse(this JsonClient client, Dictionary<string, string> queryParams,
            string cachePath = null)
        {
            yield return client.GetJsonAction<TorrentResponse>(
                @"torrent",
                client.GetSessionKey(),
                cachePath: cachePath,
                queryParams: queryParams);
        }

        private static string GetTitle(
            this Torrent torrent,
            JsonClient client)
        {
            return torrent.GetTitle(client.GetGroups, client.GetArtists);
        }

        private static string GetTitle(
            this Torrent torrent, 
            Func<int, IEnumerable<Group>> groupSource,
            Func<int, IEnumerable<Artist>> artistSource)
        {
            var group = groupSource(torrent.GroupId).Single();
            
            if (group.Category != @"Music")
            {
                return $@"{group.Category} - {torrent.FilePath}";
            }

            var sb = new StringBuilder();

            if (!group.ReleaseType.Equals(ReleaseType.Soundtrack) && !group.ReleaseType.Equals(ReleaseType.Compilation))
            {
                var artist = artistSource(group.ArtistIds.First()).Single();
                sb.Append($"{artist.Genre} - {artist.Name} - ");
            }

            sb.Append($@"{group.ReleaseType.Description()} - {group.Year} - {group.Name.Replace(@" - ", @": ")}");

            if (torrent.Remastered)
            {
                if (!string.IsNullOrWhiteSpace(torrent.RemasterTitle))
                {
                    sb.Append($@" ({torrent.RemasterTitle})");
                }
                if (!string.IsNullOrWhiteSpace(torrent.RemasterCatalogueNumber))
                {
                    sb.Append($@" {{{torrent.RemasterCatalogueNumber}}}");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(group.CatalogueNumber))
                {
                    sb.Append($@" {{{group.CatalogueNumber}}}");
                }
            }

            sb.Append($@" [{torrent.Media} {torrent.Format} {torrent.Encoding}]".Replace(@"[CD ", @"[").Replace(@" Lossless]", @"]"));

            return sb.ToString().Replace(":", "_").Replace(@"/", " ").Replace(@"\", " ").Replace(@"   ", " ").Replace(@"  ", @" ");
        }

        private class TorrentResponse
        {
            [JsonProperty(@"torrent")]
            public Torrent Torrent { get; set; }

            [JsonProperty(@"group")]
            public ObjectWithId Group { get; set; }
        }
    }
}