using BuffBar.Constants;
using BuffBar.Maps;
using BuffBar.Models;
using BuffBar.Utils;
using UnityEngine;

namespace BuffBar.UI;

public class OverlayUI
{
    private bool HasMinions => _minions.Count > 0;
    private float BuffLeft => HasMinions ? Size.BUFF_LEFT_OFFSET : Size.MARGIN;

    private readonly GUIStyle _style;
    private readonly List<MinionState> _minions = new();
    private readonly List<ShieldState> _shields = new();
    private readonly List<StatState> _stats = new();

    private float _currBuffPos;
    private float _currMinionPos;

    public OverlayUI(Font font)
    {
        _style = GUIStyles.OverlayText;
        _style.font = font;
    }

    public void Send(MinionState minion) => _minions.Add(minion);
    public void Send(ShieldState shield) => _shields.Add(shield);
    public void Send(StatState stat) => _stats.Add(stat);

    public void PurgeState()
    {
        _minions.Clear();
        _shields.Clear();
        _stats.Clear();
    }

    public void OnGUI()
    {
        _currBuffPos = Size.MARGIN;
        _currMinionPos = Size.MARGIN;
        foreach (var minion in _minions)
            GUIPushMinion(minion.Name, minion.Count);
        foreach (var shield in _shields)
            GUIPushShield(shield.Name, shield.TimeLeft, shield.MaxTime, shield.HpLeft);
        foreach (var stat in _stats)
            GUIPushStat(stat.Name, stat.TimeLeft, stat.MaxTime);
    }

    private void GUIPushMinion(string minionName, int count)
    {
        var boxPos = new Vector2(Size.MARGIN, _currMinionPos);
        PushIcon(minionName, boxPos);
        PushTextOverlay(boxPos, count.ToString());
        _currMinionPos += Size.ICON + Size.MARGIN;
    }

    private void GUIPushStat(string statName, float timeLeft, float maxTime)
    {
        var boxPos = new Vector2(BuffLeft, _currBuffPos);
        PushIcon(statName, boxPos);
        PushProgressOverlay(boxPos, timeLeft / maxTime);
        _currBuffPos += Size.ICON + Size.MARGIN;
    }

    private void GUIPushShield(string statName, float timeLeft, float maxTime, float hpLeft)
    {
        var boxPos = new Vector2(BuffLeft, _currBuffPos);
        PushIcon(statName, boxPos);
        PushProgressOverlay(boxPos, timeLeft / maxTime);
        PushTextOverlay(boxPos, $"{hpLeft:0}");
        _currBuffPos += Size.ICON + Size.MARGIN;
    }

    private void PushIcon(string iconName, Vector2 position) =>
        GUI.Box(new Rect(position, Size.IconSize), IconMap.Get(iconName));

    private void PushProgressOverlay(Vector2 topLeft, float percent)
    {
        var bottomLeft = topLeft with { y = topLeft.y + Size.ICON };
        var lerpedSize = Size.IconSize with { y = Mathf.Lerp(0, Size.ICON, percent) };
        GUI.Box(new Rect(bottomLeft, -lerpedSize), GUIContent.none, GUIStyles.OverlayTimer);
    }

    private void PushTextOverlay(Vector2 topLeft, string text)
    {
        var labelPos = topLeft with
        {
            x = topLeft.x + Size.MARGIN * 2,
            y = topLeft.y - Size.FONT_VERT_OFFSET + Size.ICON - Size.FONT_VERT_OFFSET - Size.MARGIN
        };
        GUIExt.DrawTextWithOutline(new Rect(labelPos, Size.IconSize), text, 2, _style);
    }
}