using UnityEngine;

namespace BuffBar.Maps;

public static class IconMap
{
    private static IReadOnlyDictionary<string, Texture2D>? _icons;

    public static int LoadIcons()
    {
        if (_icons != null) return _icons.Count;
        var dict = new Dictionary<string, Texture2D>();
        // ReSharper disable once Unity.UnknownResource
        var icons = Resources.LoadAll<Texture2D>("power-ups-icon");
        foreach (var icon in icons)
        {
            icon.hideFlags = HideFlags.HideAndDontSave;
            var name = icon.name.ToLower();
            dict[name] = icon;
        }
        _icons = dict;
        return _icons.Count;
    }

    public static Texture2D Get(string name) => _icons![name.ToLower()];
}