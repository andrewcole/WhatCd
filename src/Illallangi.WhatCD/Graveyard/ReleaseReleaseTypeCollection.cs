using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Illallangi.WhatCD.Model.Groups;
using Illallangi.WhatCD.Model.Torrents;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Graveyard
{
    public class ReleaseReleaseTypeCollection : ObservableCollection<KeyValuePair<int, ReleaseType>>
    {
        public static ReleaseReleaseTypeCollection Load()
        {
            var path = Environment.ExpandEnvironmentVariables(@"%localappdata%\Illallangi Enterprises\WhatCd Client\releaseTypes.json");
            var releaseReleaseTypeCollection = (File.Exists(path) ?
                JsonConvert.DeserializeObject<ReleaseReleaseTypeCollection>(File.ReadAllText(path)) :
                null) ?? new ReleaseReleaseTypeCollection();

            releaseReleaseTypeCollection.CollectionChanged += (sender, args) => releaseReleaseTypeCollection.Save();

            return releaseReleaseTypeCollection;
        }

        private void Save()
        {
            var path = Environment.ExpandEnvironmentVariables(@"%localappdata%\Illallangi Enterprises\WhatCd Client\releaseTypes.json");
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
    }
}