using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Win32;

namespace Illallangi.WhatCD.Model.Profiles
{
    public sealed class Profile
    {
        public static IEnumerable<Profile> GetProfiles()
        {
            var subKeyNames = Registry.CurrentUser.CreateSubKey($@"Software\Illallangi Enterprises\WhatCd Client\Profiles")
                .GetSubKeyNames();
            return subKeyNames
                .Select(key => new Profile { Key = key });
        }

        public static Profile GetActiveProfile()
        {
            return Profile.GetProfiles().Single(p => p.Active);
        }

        public static void DeleteProfile(string key)
        {
            Registry.CurrentUser.DeleteSubKey($@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{key}");
        }

        private string currentIndex;

        public string Key
        {
            get { return this.currentIndex ?? (this.currentIndex = Guid.NewGuid().ToString()); }
            set { this.currentIndex = value; }
        }

        public string Name
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    string.Empty,
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    string.Empty,
                    value);
            }
        }

        public string Uri
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"Uri",
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"Uri",
                    value);
            }
        }

        public string Username
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"Username",
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"Username",
                    value);
            }
        }


        public string Password
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"Password",
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"Password",
                    value);
            }
        }

        public bool Active
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"Active",
                    @"False").Equals(@"True");
            }

            set
            {
                if (value)
                {
                    foreach (var profile in Profile.GetProfiles())
                    {
                        profile.Active = false;
                    }
                }

                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"Active",
                    value.ToString());

            }
        }

        public string SessionKey
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"SessionKey",
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"SessionKey",
                    value);
            }
        }

        public DateTime SessionExpiry
        {
            get
            {
                return DateTime.ParseExact(Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"SessionExpiry",
                    DateTime.MinValue.ToString(@"O", CultureInfo.InvariantCulture)), @"O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}",
                    @"SessionExpiry",
                    value.ToString(@"O", CultureInfo.InvariantCulture));
            }
        }


        public DateTime Query5
        {
            get
            {
                return DateTime.ParseExact(Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"5",
                    DateTime.MinValue.ToString(@"O", CultureInfo.InvariantCulture)), @"O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"5",
                    value.ToString(@"O", CultureInfo.InvariantCulture));
            }
        }

        public DateTime Query4
        {
            get
            {
                return DateTime.ParseExact(Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"4",
                    DateTime.MinValue.ToString(@"O", CultureInfo.InvariantCulture)), @"O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"4",
                    value.ToString(@"O", CultureInfo.InvariantCulture));
            }
        }


        public DateTime Query3
        {
            get
            {
                return DateTime.ParseExact(Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"3",
                    DateTime.MinValue.ToString(@"O", CultureInfo.InvariantCulture)), @"O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"3",
                    value.ToString(@"O", CultureInfo.InvariantCulture));
            }
        }


        public DateTime Query2
        {
            get
            {
                return DateTime.ParseExact(Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"2",
                    DateTime.MinValue.ToString(@"O", CultureInfo.InvariantCulture)), @"O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"2",
                    value.ToString(@"O", CultureInfo.InvariantCulture));
            }
        }


        public DateTime Query1
        {
            get
            {
                return DateTime.ParseExact(Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"1",
                    DateTime.MinValue.ToString(@"O", CultureInfo.InvariantCulture)), @"O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\WhatCd Client\Profiles\{this.Key}\Queries",
                    @"1",
                    value.ToString(@"O", CultureInfo.InvariantCulture));
            }
        }

        public override string ToString()
        {
            return $"WhatCd Profile {this.Name} ({this.Uri})";
        }

        public void DelayIfRequired(int seconds = 10)
        {
            while (DateTime.Now.AddSeconds(0 - seconds) < this.Query5)
            {
                Thread.Sleep(1000);
            }

            this.Query5 = this.Query4;
            this.Query4 = this.Query3;
            this.Query3 = this.Query2;
            this.Query2 = this.Query1;
            this.Query1 = DateTime.Now;
        }
    }
}