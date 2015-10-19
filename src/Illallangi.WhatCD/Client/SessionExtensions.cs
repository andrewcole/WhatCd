using System;
using Illallangi.WhatCD.Model.Profiles;

namespace Illallangi.WhatCD.Client
{
    public static class SessionExtensions
    {
        public static bool CheckSession(this JsonClient client)
        {
            throw new NotImplementedException();
        }

        public static string GetSessionKey(this JsonClient client)
        {
            var profile = Profile.GetActiveProfile();

            if (profile.SessionExpiry > DateTime.Now)
            {
                return profile.SessionKey;
            }

            string session;
            DateTime expiry;
             
            if (!client.Login(out session, out expiry))
            {
                throw new Exception("Logon failed");
            }

            try
            {
                if (!client.CheckSession())
                {
                    throw new Exception(@"Session not OK after logon.");
                }
            }
            catch (NotImplementedException)
            {
                //noop
            }
            
            profile.SessionKey = session;
            profile.SessionExpiry = expiry;

            return session;
        }
    }
}