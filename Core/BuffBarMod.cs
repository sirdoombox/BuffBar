﻿using BuffBar.Collections;
using BuffBar.Constants;
using BuffBar.Extensions;
using BuffBar.Maps;
using BuffBar.Models;
using BuffBar.UI;
using BuffBar.Utils;
using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace BuffBar.Core;

// TODO: More tidying up everywhere
public class BuffBarMod : MelonMod
{
    private readonly SkillEffectCollection<TemporaryShieldWrapper, TemporaryShieldOnActivationEffect> _shields = new();

    private readonly SkillEffectCollection<TemporaryStatsWrapper, TemporaryStatsOnActivationEffect> _stats = new();

    private readonly Dictionary<string, int> _followers = new();

    private OverlayUI _ui = null!;

    private HealthComponent _health = null!;

    private bool _inGame;

    public override void OnInitializeMelon()
    {
        var loaded = IconMap.LoadIcons();
        LoggerInstance.Msg($"Icon Map Loaded: {loaded} icons available.");
    }

    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        _inGame = false;
        if (buildIndex == 0) // splash screen
        {
            var font = Object.FindObjectOfType<Canvas>()
                .gameObject
                .GetComponentInChildren<TextMeshProUGUI>()
                .font
                .sourceFontFile;
            LoggerInstance.Msg($"Got Font {font.name}");
            GUIStyles.OverlayText.font = font;
            _ui = new OverlayUI();
        }

        if (buildIndex <= 2) return; // ignore loading (0) and main menu (2)
        _inGame = true;
        VersionLabelUtility.Disable();
        _stats.Clear();
        _shields.Clear();
        _health = GameManagerUtil.SurvivorsGameManager.PlayerEntity._health;
    }

    public void AddEffect(TemporaryShieldOnActivationEffect effect, Skill skill) =>
        _shields.Add(new TemporaryShieldWrapper(effect, skill));

    public void AddEffect(TemporaryStatsOnActivationEffect effect, Skill skill) =>
        _stats.Add(new TemporaryStatsWrapper(effect, skill));

    public void UpdateDurationOfEffect(TemporaryShieldOnActivationEffect effect, float duration) =>
        _shields.UpdateDuration(effect, duration);

    public void UpdateDurationOfEffect(TemporaryStatsOnActivationEffect effect, float duration) =>
        _stats.UpdateDuration(effect, duration);
    
    // BUG: On first run this seems to cause a nullref and I'm not sure why...
    public override void OnUpdate()
    {
        if (!_inGame) return;
        if (Keyboard.current.f11Key.wasPressedThisFrame)
        {
            LevelUpUtility.AddLevels(10);
            LevelUpUtility.AddRerolls(50);
        }
        _ui.PurgeState();
        foreach (var stat in _stats)
        {
            if (!stat.HasEffects) continue;
            _ui.Send(new StatState(stat.Name, stat.MaxDuration, stat.Effect.RemainingDuration));
        }

        foreach (var shield in _shields)
        {
            if (!shield.HasEffects) continue;
            var tempShield = shield.Effect;
            var tempShieldApplied =
                _health.TemporaryShields.Il2CppFirstOrDefault(x => x.Id == tempShield.TemporaryStatsId);
            _ui.Send(new ShieldState(shield.Name, shield.MaxDuration, tempShield.RemainingDuration,
                tempShieldApplied!.ShieldHealth));
        }

        _followers.Clear();
        foreach (var follower in GameManagerUtil.SurvivorsGameManager.PlayerEntity._followers.Followers)
        {
            var entity = follower.Follower;
            if (!_followers.TryAdd(entity.EntityName, 1))
                _followers[entity.EntityName]++;
        }

        foreach (var follower in _followers)
        {
            var name = follower.Key;
            if (name.EndsWith("Title"))
                name = name[..^5];
            _ui.Send(new MinionState(name, follower.Value));
        }
    }

    public override void OnGUI()
    {
        if (!_inGame) return;
        _ui.OnGUI();
    }
}