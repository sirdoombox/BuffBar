﻿using MelonLoader;
using SoulstoneSurvivorsMods.BuffOverlay.Maps;
using SoulstoneSurvivorsMods.BuffOverlay.Models;
using SoulstoneSurvivorsMods.BuffOverlay.Utils;
using UnityEngine;

namespace SoulstoneSurvivorsMods.BuffOverlay.UI;

public class OverlayUI
{
    private static readonly GUIStyle OverlayTimerStyle = new(GUI.skin.box)
    {
        normal =
        {
            background = TextureUtility.BarTexture
        },
        margin = new RectOffset(0, 0, 0, 0),
        overflow = new RectOffset(0, 0, 0, 0),
        fixedWidth = ResUtility.GetWidth(ICON_SIZE),
        fixedHeight = 0,
        stretchWidth = false,
        stretchHeight = false,
        padding = new RectOffset(0, 0, 0, 0),
        border = new RectOffset(0, 0, 0, 0),
    };

    private readonly GUIStyle _style;
    private readonly List<MinionState> _minions = new();
    private readonly List<ShieldState> _shields = new();
    private readonly List<StatState> _stats = new();

    private bool HasMinions => _minions.Count > 0;
    private float BuffLeft => HasMinions ? (MARGIN * 2) + ICON_SIZE : MARGIN;
    private static readonly Vector2 IconSize = new(ICON_SIZE, ICON_SIZE);

    private float _currBuffPos;
    private float _currMinionPos;
    private const float ICON_SIZE = 50;
    private const float MARGIN = 5;
    private const int FONT_SIZE = 50;
    private const float FONT_VERT_OFFSET = FONT_SIZE * 0.625f;
    private const float LABEL_OFFSET = ICON_SIZE / 4f;

    public void Send(MinionState minion) => _minions.Add(minion);
    public void Send(ShieldState shield) => _shields.Add(shield);
    public void Send(StatState stat) => _stats.Add(stat);

    public OverlayUI(GUIStyle style)
    {
        _style = style;
        _style.fontSize = FONT_SIZE;
    }

    public void PurgeState()
    {
        _minions.Clear();
        _shields.Clear();
        _stats.Clear();
    }

    public void OnGUI()
    {
        _currBuffPos = MARGIN;
        _currMinionPos = MARGIN;
        foreach (var minion in _minions)
            GUIPushMinion(minion.Name, minion.Count);
        foreach (var shield in _shields)
            GUIPushShield(shield.Name, shield.TimeLeft, shield.MaxTime, shield.HpLeft);
        foreach (var stat in _stats)
            GUIPushStat(stat.Name, stat.TimeLeft, stat.MaxTime);
    }

    private void GUIPushMinion(string minionName, int count)
    {
        var boxPos = new Vector2(MARGIN, _currMinionPos);
        PushIcon(minionName, boxPos);
        PushTextOverlay(boxPos, count.ToString());
        _currMinionPos += ICON_SIZE + MARGIN;
    }

    private void GUIPushStat(string statName, float timeLeft, float maxTime)
    {
        var boxPos = new Vector2(BuffLeft, _currBuffPos);
        PushIcon(statName, boxPos);
        PushProgressOverlay(boxPos, timeLeft / maxTime);
        _currBuffPos += ICON_SIZE + MARGIN;
    }

    private void GUIPushShield(string statName, float timeLeft, float maxTime, float hpLeft)
    {
        var boxPos = new Vector2(BuffLeft, _currBuffPos);
        PushIcon(statName, boxPos);
        PushProgressOverlay(boxPos, timeLeft / maxTime);
        PushTextOverlay(boxPos, $"{hpLeft:0}");
        _currBuffPos += ICON_SIZE + MARGIN;
    }

    private void PushIcon(string iconName, Vector2 position)
    {
        GUI.Box(new Rect(position, IconSize), IconMap.Get(iconName));
    }

    private void PushProgressOverlay(Vector2 topLeft, float percent)
    {
        var bottomLeft = topLeft with { y = topLeft.y + ICON_SIZE };
        var lerpedSize = IconSize with { y = Mathf.Lerp(0, ICON_SIZE, percent) };
        GUI.Box(new Rect(bottomLeft, -lerpedSize), GUIContent.none, OverlayTimerStyle);
    }

    private void PushTextOverlay(Vector2 topLeft, string text)
    {
        var labelPos = topLeft with { x = topLeft.x + LABEL_OFFSET, y = topLeft.y - FONT_VERT_OFFSET + LABEL_OFFSET };
        GUI.Label(new Rect(labelPos, IconSize), text, _style);
    }
}