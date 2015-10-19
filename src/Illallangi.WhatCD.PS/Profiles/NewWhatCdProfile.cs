using System.Management.Automation;
using Illallangi.WhatCD.Model.Profiles;

namespace Illallangi.WhatCD.PS.Profiles
{
    [Cmdlet(VerbsCommon.New, @"WhatCdProfile")]
    public sealed class NewWhatCdProfile : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        public string Username { get; set; }

        [Parameter(Mandatory = true)]
        public string Uri { get; set; }

        [Parameter(Mandatory = true)]
        public string Password { get; set; }

        [Parameter]
        public SwitchParameter Active { get; set; }

        protected override void ProcessRecord()
        {
            this.WriteObject(new Profile { Name = this.Name, Username = this.Username, Uri = this.Uri, Password = this.Password, Active = this.Active.IsPresent });
        }
    }
}