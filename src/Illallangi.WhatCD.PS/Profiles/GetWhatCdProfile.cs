using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Illallangi.WhatCD.Model.Profiles;

namespace Illallangi.WhatCD.PS.Profiles
{
    [Cmdlet(VerbsCommon.Get, @"WhatCdProfile")]
    public class GetWhatCdProfile : PSCmdlet
    {
        private string currentUri;
        private string currentName;
        private string currentUsername;
        private WildcardPattern currentUriWildcardPattern;
        private WildcardPattern currentNameWildcardPattern;
        private WildcardPattern currentUsernameWildcardPattern;

        [Parameter()]
        public string Name
        {
            get
            {
                return this.currentName;
            }
            set
            {
                this.currentName = value;
                this.currentNameWildcardPattern = null;
            }
        }

        [Parameter()]
        public string Username
        {
            get
            {
                return this.currentUsername;
            }
            set
            {
                this.currentUsername = value;
                this.currentUsernameWildcardPattern = null;
            }
        }

        [Parameter()]
        public string Uri
        {
            get
            {
                return this.currentUri;
            }
            set
            {
                this.currentUri = value;
                this.currentUriWildcardPattern = null;
            }
        }

        [Parameter]
        public SwitchParameter Active { get; set; }

        private WildcardPattern NameWildcardPattern => this.currentNameWildcardPattern
                                                              ?? (this.currentNameWildcardPattern =
                                                                  new WildcardPattern(this.Name ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        private WildcardPattern UsernameWildcardPattern => this.currentUsernameWildcardPattern
                                                              ?? (this.currentUsernameWildcardPattern =
                                                                  new WildcardPattern(this.Username ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        private WildcardPattern UriWildcardPattern => this.currentUriWildcardPattern
                                                              ?? (this.currentUriWildcardPattern =
                                                                  new WildcardPattern(this.Uri ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));
        
        protected override void ProcessRecord()
        {
            this.WriteObject(this.GetWhatCdProfiles(), true);
        }

        private IEnumerable<Profile> GetWhatCdProfiles()
        {
            return Profile.GetProfiles().Where(this.IsMatch);
        }

        protected virtual bool IsMatch(Profile profile)
        {
            return this.NameWildcardPattern.IsMatch(profile.Name) &&
                   this.UriWildcardPattern.IsMatch(profile.Uri) &&
                   this.UsernameWildcardPattern.IsMatch(profile.Username) &&
                   (!this.Active.IsPresent || profile.Active);
        }
    }
}
