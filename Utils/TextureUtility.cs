using UnityEngine;

namespace BuffBar.Utils;

public abstract class TextureUtility
{
    private static readonly Color BarColor = new(0.5f, 0.5f, 0.5f, 0.7f);

    /// <summary>
    ///     Blank texture for overlays.
    /// </summary>
    public static Texture2D BarTexture
    {
        get
        {
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, BarColor);
            texture.wrapMode = TextureWrapMode.Repeat;
            texture.filterMode = FilterMode.Point;
            texture.Apply();
            texture.hideFlags = HideFlags.DontSave;
            return texture;
        }
    }
}