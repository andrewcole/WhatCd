using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Model.Artists
{
    public class TorrentResponseTitleCollection : ObservableCollection<KeyValuePair<string, string>>
    {
        public static TorrentResponseTitleCollection Load()
        {
            var path = Environment.ExpandEnvironmentVariables(@"%localappdata%\Illallangi Enterprises\WhatCd Client\titles.json");
            var torrentResponseTitleCollection = (File.Exists(path) ?
                JsonConvert.DeserializeObject<TorrentResponseTitleCollection>(File.ReadAllText(path)) :
                null) ?? new TorrentResponseTitleCollection();

            torrentResponseTitleCollection.CollectionChanged += (sender, args) => torrentResponseTitleCollection.Save();

            return torrentResponseTitleCollection;
        }

        private void Save()
        {
            var path = Environment.ExpandEnvironmentVariables(@"%localappdata%\Illallangi Enterprises\WhatCd Client\titles.json");
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
    }
}