using UnityEngine;

namespace SoulstoneSurvivorsMods.BuffOverlay.Utils;

public static class VersionLabel
{
    private const string VERSION_LABEL = "Small-Version-Label";

    public static void Disable() => GameObject.Find(VERSION_LABEL)?.SetActive(false);
}