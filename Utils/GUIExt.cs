using UnityEngine;

namespace BuffBar.Utils;

public static class GUIExt
{
    public static void DrawTextWithOutline(Rect r, string t, int strength, GUIStyle style)
    {
        GUI.color = new Color(0, 0, 0, 1);
        int i;
        for (i = -strength; i <= strength; i++)
        {
            GUI.Label(new Rect(r.x - strength, r.y + i, r.width, r.height), t, style);
            GUI.Label(new Rect(r.x + strength, r.y + i, r.width, r.height), t, style);
        }

        for (i = -strength + 1; i <= strength - 1; i++)
        {
            GUI.Label(new Rect(r.x + i, r.y - strength, r.width, r.height), t, style);
            GUI.Label(new Rect(r.x + i, r.y + strength, r.width, r.height), t, style);
        }

        GUI.color = new Color(1, 1, 1, 1);
        GUI.Label(r, t, style);
    }
}