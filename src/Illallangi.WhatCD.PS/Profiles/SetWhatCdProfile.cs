using System.Linq;
using System.Management.Automation;
using Illallangi.WhatCD.Model.Profiles;

namespace Illallangi.WhatCD.PS.Profiles
{
    [Cmdlet(VerbsCommon.Set, @"WhatCdProfile")]
    public sealed class SetWhatCdProfile : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Key { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Username { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Uri { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Password { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Active { get; set; }

        protected override void ProcessRecord()
        {
            foreach (var profile in Profile.GetProfiles().Where(p => p.Key.Equals(this.Key)))
            {
                profile.Name = this.Name ?? profile.Name;
                profile.Username = this.Username ?? profile.Username;
                profile.Uri = this.Uri ?? profile.Uri;
                profile.Password = this.Password ?? profile.Password;
                if (this.Active.IsPresent)
                {
                    profile.Active = true;
                }
            }

            this.WriteObject(Profile.GetProfiles().Where(p => p.Key.Equals(this.Key)));
        }
    }
}
