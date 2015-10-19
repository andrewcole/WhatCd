namespace Illallangi.WhatCD.Model.Groups
{
    public enum ReleaseType
    {
        Album = 1,
        Soundtrack = 3,
        EP = 5,
        Anthology = 6,
        Compilation = 7,
        Single = 9,
        LiveAlbum = 11,
        Remix = 13,
        Bootleg = 14,
    }

    public static class ReleaseTypeExtensions
    {
        public static string Description(this ReleaseType t)
        {
            switch (t)
            {
                case ReleaseType.LiveAlbum:
                    return @"Live Album";
                default:
                    return t.ToString();
            }
        }
    }
}