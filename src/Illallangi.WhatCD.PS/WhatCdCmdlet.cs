using System.Management.Automation;
using Illallangi.WhatCD.Client;
using Illallangi.WhatCD.Model.Profiles;

namespace Illallangi.WhatCD.PS
{
    public abstract class WhatCdCmdlet : PSCmdlet
    {
        private JsonClient currentClient;
        protected JsonClient Client
        {
            get
            {
                if (this.currentClient == null)
                {
                    this.currentClient = new JsonClient(Profile.GetActiveProfile().Uri);
                    this.currentClient.Querying += (o, args) =>
                    {
                        Profile.GetActiveProfile().DelayIfRequired();
                    };
                }
                return this.currentClient;
            }
        }
    }
}