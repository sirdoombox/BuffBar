using UnityEngine;

namespace BuffBar.Utils;

public abstract class TextureUtility
{
    public static Texture2D GetTexture(Color? color = null)
    {
        color ??= Color.white;
        var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture.SetPixel(0, 0, color.Value);
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        texture.hideFlags = HideFlags.DontSave;
        return texture;
    }
}