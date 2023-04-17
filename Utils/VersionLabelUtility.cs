using UnityEngine;

namespace BuffBar.Utils;

public static class VersionLabelUtility
{
    private const string VERSION_LABEL_NAME = "Small-Version-Label";

    public static void Disable() => GameObject.Find(VERSION_LABEL_NAME)?.SetActive(false);
}