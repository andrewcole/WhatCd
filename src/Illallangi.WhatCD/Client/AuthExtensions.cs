using System;
using System.Collections.Generic;
using Illallangi.WhatCD.Model.Profiles;

namespace Illallangi.WhatCD.Client
{
    public static class AuthExtensions
    {
        public static bool Login(this JsonClient client, string username = null, string password = null)
        {
            string session;
            DateTime expiry;
            return client.Login(out session, out expiry, username, password);
        }

        public static bool Login(this JsonClient client, out string session, out DateTime expiry, string username = null, string password = null)
        {
            var result = client.PostCookieAction(
                postParams: new Dictionary<string, string>
                {
                    { @"username", username ?? Profile.GetActiveProfile().Username },
                    { @"password", password ?? Profile.GetActiveProfile().Password },
                    { @"keeplogged", @"1" }
                });
            var cookie = result.ContainsKey(@"session") ? result[@"session"] : null;
            session = cookie?.Item1;
            expiry = cookie?.Item2 ?? DateTime.MinValue;
            return !string.IsNullOrWhiteSpace(session);
        }
    }
}