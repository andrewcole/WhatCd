using System;
using System.Diagnostics;
using Illallangi.WhatCD.Client;
using NUnit.Framework;

namespace Illallangi.WhatCD.Test
{
    [TestFixture]
    public sealed class WhatCdClientAuthTests : WhatCdClientTestBase
    {
        [Test]
        public void LoginReturnsTrue()
        {
            Assert.IsTrue(this.Client.Login());
        }

        [Test]
        public void LoginReturnsSessionKey()
        {
            string session;
            DateTime expiry;
            var result = this.Client.Login(out session, out expiry);
            Assert.IsTrue(result, @"Logon attempt failed.");
            Assert.IsNotNullOrEmpty(session);
            Assert.IsNotNull(expiry);
            Assert.Greater(expiry, DateTime.Now, @"Session Key expires in the past.");
            Debug.WriteLine($"Session cookie returned {session} with an expiry of {expiry}");
        }

        [Test]
        public void LoginReturnsFalse()
        {
            Assert.IsFalse(this.Client.Login(password: Guid.NewGuid().ToString()));
        }

        [Test]
        public void LogonReturnsSessionKey()
        {
            Assert.NotNull(this.Client.GetSessionKey());
        }
    }
}
