using System.Linq;
using System.Management.Automation;
using Illallangi.WhatCD.Model.Profiles;

namespace Illallangi.WhatCD.PS.Profiles
{
    [Cmdlet(VerbsCommon.Remove, @"WhatCdProfile", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveWhatCdProfile : GetWhatCdProfile
    {
        protected override void ProcessRecord()
        {
            foreach (var profile in Profile.GetProfiles().Where(this.IsMatch))
            {
                Profile.DeleteProfile(profile.Key);
            }
        }

        protected override bool IsMatch(Profile profile)
        {
            return base.IsMatch(profile) &&
                   this.ShouldProcess(profile.ToString(), VerbsCommon.Remove);
        }
    }
}