using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.WhatCD.Client;
using Illallangi.WhatCD.Model.Groups;

namespace Illallangi.WhatCD.PS.Groups
{
    [Cmdlet(VerbsCommon.Get, @"WhatCdGroup", DefaultParameterSetName = GetWhatCdGroup.HashParameterSet)]
    public sealed class GetWhatCdGroup : WhatCdGetCmdlet<Group>
    {
        private const string IdParameterSet = @"Id";
        private const string HashParameterSet = @"Hash";

        [Parameter(Mandatory = true, ParameterSetName = GetWhatCdGroup.IdParameterSet)]
        public int Id { get; set; }
        
        [Parameter(Mandatory = true, ParameterSetName = GetWhatCdGroup.HashParameterSet)]
        public string Hash { get; set; }

        protected override IEnumerable<Group> GetEnumerable()
        {
            switch (this.ParameterSetName)
            {
                case GetWhatCdGroup.HashParameterSet:
                    return this.Client.GetGroups(this.Hash);
                case GetWhatCdGroup.IdParameterSet:
                    return this.Client.GetGroups(this.Id);
                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
        }
        protected override bool IsMatch(Group obj)
        {
            switch (this.ParameterSetName)
            {
                case GetWhatCdGroup.HashParameterSet:
                    return true;
                case GetWhatCdGroup.IdParameterSet:
                    return this.Id.Equals(obj.GroupId);
                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
        }
    }
}