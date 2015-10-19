using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace Illallangi.WhatCD.Model.Artists
{
    public class ArtistGenreCollection : ObservableCollection<KeyValuePair<int, string>>
    {
        public static ArtistGenreCollection Load()
        {
            var path = Environment.ExpandEnvironmentVariables(@"%localappdata%\Illallangi Enterprises\WhatCd Client\genres.json");
            var artistGenreCollection = (File.Exists(path) ?
                JsonConvert.DeserializeObject<ArtistGenreCollection>(File.ReadAllText(path)) :
                null) ?? new ArtistGenreCollection();

            artistGenreCollection.CollectionChanged += (sender, args) => artistGenreCollection.Save();

            return artistGenreCollection;
        }

        private void Save()
        {
            var path = Environment.ExpandEnvironmentVariables(@"%localappdata%\Illallangi Enterprises\WhatCd Client\genres.json");
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
    }
}