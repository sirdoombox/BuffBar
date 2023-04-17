using UnityEngine;

namespace SoulstoneSurvivorsMods.BuffOverlay.Utils;

public class TextureUtility
{
    private static readonly Color BarColor = new(0.42f, 0.42f, 0.42f, 0.5f);
	
    /// <summary>
    /// Texture for the Bar
    /// </summary>
    public static Texture2D BarTexture
    {
        get
        {
            // create a 1x1 texture with red color
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0,0, BarColor);
            texture.wrapMode = TextureWrapMode.Repeat;
            texture.filterMode = FilterMode.Point;
            // apply the changes
            texture.Apply();
            texture.hideFlags = HideFlags.DontSave;
            // return the texture
            return texture;
        }
    }
}