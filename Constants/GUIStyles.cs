using BuffBar.Utils;
using UnityEngine;

namespace BuffBar.Constants;

public static class GUIStyles
{
    public static readonly GUIStyle OverlayTimer = new(GUI.skin.box)
    {
        normal =
        {
            background = TextureUtility.GetTexture()
        },
        margin = new RectOffset(0, 0, 0, 0),
        overflow = new RectOffset(0, 0, 0, 0),
        fixedWidth = ResUtility.GetWidth(Size.ICON),
        fixedHeight = 0,
        stretchWidth = false,
        stretchHeight = false,
        padding = new RectOffset(0, 0, 0, 0),
        border = new RectOffset(0, 0, 0, 0)
    };

    public static readonly GUIStyle OverlayText = new(GUI.skin.label)
    {
        fontSize = Size.FONT,
        normal = new GUIStyleState { textColor = Color.white }
    };
}