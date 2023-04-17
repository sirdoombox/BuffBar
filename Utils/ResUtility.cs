using UnityEngine;

namespace BuffBar.Utils;

public static class ResUtility
{
    private const float REFERENCE_WIDTH = 1920f;
    private const float REFERENCE_HEIGHT = 1080f;

    public static float GetWidth(float width) => width * (Screen.width / REFERENCE_WIDTH);

    public static float GetHeight(float height) => height * (Screen.height / REFERENCE_HEIGHT);

    public static int GetFontSize(int i) => (int)(i * (Screen.height / REFERENCE_HEIGHT));
}