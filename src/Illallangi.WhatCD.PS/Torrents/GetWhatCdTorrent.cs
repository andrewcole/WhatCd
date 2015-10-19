using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.WhatCD.Client;
using Illallangi.WhatCD.Model.Torrents;

namespace Illallangi.WhatCD.PS.Torrents
{
    [Cmdlet(VerbsCommon.Get, @"WhatCdTorrent", DefaultParameterSetName = GetWhatCdTorrent.HashParameterSet)]
    public sealed class GetWhatCdTorrent : WhatCdGetCmdlet<Torrent>
    {
        private const string IdParameterSet = @"Id";
        private const string HashParameterSet = @"Hash";

        [Parameter(Mandatory = true, ParameterSetName = GetWhatCdTorrent.IdParameterSet)]
        public int Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetWhatCdTorrent.HashParameterSet)]
        public string Hash { get; set; }

        protected override IEnumerable<Torrent> GetEnumerable()
        {
            switch (this.ParameterSetName)
            {
                case GetWhatCdTorrent.HashParameterSet:
                    return this.Client.GetTorrents(this.Hash);
                case GetWhatCdTorrent.IdParameterSet:
                    return this.Client.GetTorrents(this.Id);
                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
        }
        protected override bool IsMatch(Torrent obj)
        {
            switch (this.ParameterSetName)
            {
                case GetWhatCdTorrent.HashParameterSet:
                    return true;
                case GetWhatCdTorrent.IdParameterSet:
                    return this.Id.Equals(obj.TorrentId);
                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
        }
    }
}