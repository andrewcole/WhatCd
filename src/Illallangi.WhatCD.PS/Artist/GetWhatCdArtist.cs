using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Illallangi.WhatCD.Client;
using Illallangi.WhatCD.Model.Artists;

namespace Illallangi.WhatCD.PS.Artist
{
    [Cmdlet(VerbsCommon.Get, @"WhatCdArtist")]
    public sealed class GetWhatCdArtist : WhatCdCmdlet
    {
        private const string FilterParameterSet = @"Filter";
        private const string IdParameterSet = @"Id";

        private string currentName;

        private WildcardPattern currentNameWildcardPattern;

        [Parameter(Mandatory = true, ParameterSetName = GetWhatCdArtist.IdParameterSet)]
        public int Id { get; set; }

        [SupportsWildcards]
        [Parameter(Position = 1, ParameterSetName = GetWhatCdArtist.FilterParameterSet)]
        public string Name
        {
            get
            {
                return this.currentName;
            }
            set
            {
                this.currentNameWildcardPattern = null;
                this.currentName = value;
            }
        }

        private WildcardPattern NameWildcardPattern => this.currentNameWildcardPattern
                                                       ?? (this.currentNameWildcardPattern =
                                                           new WildcardPattern(this.Name ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        private IEnumerable<Model.Artists.Artist> GetArtists()
        {
            switch (this.ParameterSetName)
            {
                case GetWhatCdArtist.IdParameterSet:
                    return this.Client.GetArtists(this.Id).Where(this.IsMatch);

                case GetWhatCdArtist.FilterParameterSet:
                    return this.Client.GetArtists(this.Name).Where(this.IsMatch);

                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
            
        }

        private bool IsMatch(Model.Artists.Artist artists)
        {
            switch (this.ParameterSetName)
            {
                case GetWhatCdArtist.IdParameterSet:
                    return this.Id.Equals(artists.Id);

                case GetWhatCdArtist.FilterParameterSet:
                    return this.NameWildcardPattern.IsMatch(artists.Name);

                default:
                    throw new PSNotImplementedException(this.ParameterSetName);

            }
        }

        protected override void BeginProcessing()
        {
            this.WriteObject(this.GetArtists(), true);
        }
    }


}