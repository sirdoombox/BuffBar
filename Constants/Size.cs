using UnityEngine;

namespace BuffBar.Constants;

public static class Size
{
    public const float ICON = 50;
    public const float MARGIN = 5;
    public const int FONT = 32;
    public const float FONT_VERT_OFFSET = FONT * 0.625f;
    public const float LABEL_OFFSET = ICON / 4f;
    public const float BUFF_LEFT_OFFSET = MARGIN * 2 + ICON;

    public static readonly Vector2 IconSize = new(ICON, ICON);
}