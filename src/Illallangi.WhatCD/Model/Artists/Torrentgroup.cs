using System.Collections.Generic;

namespace Illallangi.WhatCD.Model.Artists
{
    public class Torrentgroup
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public int groupYear { get; set; }
        public string groupRecordLabel { get; set; }
        public string groupCatalogueNumber { get; set; }
        public string groupCategoryID { get; set; }
        public List<string> tags { get; set; }
        public int releaseType { get; set; }
        public string wikiImage { get; set; }
        public bool groupVanityHouse { get; set; }
        public bool hasBookmarked { get; set; }
        public List<ArtistObject> artists { get; set; }
        public List<Torrent> torrent { get; set; }
    }
}