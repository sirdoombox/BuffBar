using BuffBar.Utils;
using UnityEngine;

namespace BuffBar.Constants;

public static class GUIStyles
{
    private static GUIStyle? _overlayTimer;
    public static GUIStyle OverlayTimer
    {
        get
        {
            _overlayTimer ??= new GUIStyle
            {
                normal = { background = TextureUtility.GetTexture(Colors.OverlayProgress) },
                margin = new RectOffset(0, 0, 0, 0),
                overflow = new RectOffset(0, 0, 0, 0),
                fixedWidth = ResUtility.GetWidth(Size.ICON),
                fixedHeight = 0,
                stretchWidth = false,
                stretchHeight = false,
                padding = new RectOffset(0, 0, 0, 0),
                border = new RectOffset(0, 0, 0, 0)
            };
            return _overlayTimer;
        }
    }

    private static GUIStyle? _overlayText;
    public static GUIStyle OverlayText
    {
        get
        {
            _overlayText ??= new GUIStyle
            {
                fontSize = Size.FONT,
                normal = new GUIStyleState { textColor = Color.white }
            };
            return _overlayText;
        }
    }
}