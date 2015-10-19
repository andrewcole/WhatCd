using System.Linq;
using System.Management.Automation;
using Illallangi.WhatCD.Client;

namespace Illallangi.WhatCD.PS.Artist
{
    [Cmdlet(VerbsCommon.Set, @"WhatCdArtist")]
    public sealed class SetWhatCdArtist : WhatCdCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public int Id { get; set; }

        [SupportsWildcards]
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Genre { get; set; }

        protected override void ProcessRecord()
        {
            foreach (var artist in this.Client.GetArtists(this.Id).Where(this.IsMatch))
            {
                artist.Genre = this.Genre;
                this.WriteObject(artist);
            }
        }

        private bool IsMatch(Model.Artists.Artist artists)
        {
            return this.Id.Equals(artists.Id);
        }

    }
}