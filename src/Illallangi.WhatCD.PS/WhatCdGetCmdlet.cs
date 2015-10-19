using System.Collections.Generic;
using System.Linq;

namespace Illallangi.WhatCD.PS
{
    public abstract class WhatCdGetCmdlet<T> : WhatCdCmdlet
    {
        protected abstract IEnumerable<T> GetEnumerable();

        protected virtual bool IsMatch(T obj)
        {
            return true;
        }

        protected override void BeginProcessing()
        {
            WriteObject(GetEnumerable().Where(IsMatch), true);
        }
    }
}