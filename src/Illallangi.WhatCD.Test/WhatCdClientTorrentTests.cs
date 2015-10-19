using Illallangi.WhatCD.Client;
using NUnit.Framework;

namespace Illallangi.WhatCD.Test
{
    [TestFixture]
    public sealed class WhatCdClientTorrentTests : WhatCdClientTestBase
    {
        private const string Hash = @"d5d192ee2f3c15943f51077b1bc399edf04183d5";

        [Test]
        public void GetTorrent()
        {
            this.Client.GetTorrents(Hash);
        }
    }
}