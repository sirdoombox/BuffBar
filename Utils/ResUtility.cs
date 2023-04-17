using UnityEngine;

namespace BuffBar.Utils;

public static class ResUtility
{
    private const float DEFAULT_WIDTH = 1920f;
    private const float DEFAULT_HEIGHT = 1080f;
	
    public static float GetWidth(float width)
    {
        return width * (Screen.width / DEFAULT_WIDTH);
    }
	
    public static float GetHeight(float height)
    {
        return height * (Screen.height / DEFAULT_HEIGHT);
    }

    public static int GetFontSize(int i)
    {
        return (int) (i * (Screen.height / DEFAULT_HEIGHT));
    }
}