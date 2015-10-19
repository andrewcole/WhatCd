using Illallangi.WhatCD.Model.Profiles;
using NUnit.Framework;

namespace Illallangi.WhatCD.Test
{
    public abstract class WhatCdClientTestBase
    {
        protected JsonClient Client { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.Client = new JsonClient(Profile.GetActiveProfile().Uri);
        }

        [TearDown]
        public void TearDown()
        {
            this.Client = null;
        }
    }
}