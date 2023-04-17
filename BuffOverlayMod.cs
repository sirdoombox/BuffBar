using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using SoulstoneSurvivorsMods.BuffOverlay.Extensions;
using SoulstoneSurvivorsMods.BuffOverlay.Maps;
using SoulstoneSurvivorsMods.BuffOverlay.Models;
using SoulstoneSurvivorsMods.BuffOverlay.UI;
using SoulstoneSurvivorsMods.BuffOverlay.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using Il2CppCollections = Il2CppSystem.Collections.Generic;
using Object = UnityEngine.Object;

namespace SoulstoneSurvivorsMods.BuffOverlay;

public class BuffOverlayMod : MelonMod
{
    private readonly Dictionary<int, TemporaryShieldWrapper> _shields = new();

    private readonly Dictionary<int, TemporaryStatsWrapper> _stats = new();

    private OverlayUI _ui = null!;

    private HealthComponent _health = null!;

    private readonly Dictionary<string, int> _followers = new();

    public override void OnInitializeMelon()
    {
        var loaded = IconMap.LoadIcons();
        LoggerInstance.Msg($"Icon Map Loaded: {loaded} icons available.");
    }

    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        if (buildIndex == 0) // splash screen
        {
            var font = Object.FindObjectOfType<Canvas>()
                .gameObject
                .GetComponentInChildren<TextMeshProUGUI>()
                .font
                .sourceFontFile;
            var style = new GUIStyle
            {
                font = font,
                fontSize = 32,
                normal = new GUIStyleState { textColor = Color.white }
            };
            _ui = new OverlayUI(style);
        }
        if (buildIndex <= 2) return; // ignore loading (0) and main menu (2)
        VersionLabel.Disable();
        _stats.Clear();
        _shields.Clear();
        _health = GameManagerUtil.SurvivorsGameManager.PlayerEntity._health;
    }

    public void AddEffect(TemporaryShieldOnActivationEffect effect, Skill skill) => 
        _shields.Add(effect.GetHashCode(), new TemporaryShieldWrapper(effect, skill));

    public void AddEffect(TemporaryStatsOnActivationEffect effect, Skill skill) => 
        _stats.Add(effect.GetHashCode(), new TemporaryStatsWrapper(effect, skill));

    public void UpdateDurationOfEffect(TemporaryShieldOnActivationEffect effect, float duration) => 
        _shields[effect.GetHashCode()].UpdateDuration(duration);

    public void UpdateDurationOfEffect(TemporaryStatsOnActivationEffect effect, float duration) => 
        _stats[effect.GetHashCode()].UpdateDuration(duration);

    // TODO: Move to "event based" and 
    // TODO: Try to overlay time + percentage for shields in some way?
    // TODO: Use some kinda fill overlay for time remaining
    // TODO: Figure out better text rendering solution for HP & counts.
    public override void OnUpdate()
    {
        if (_health == null) return;
        if (Keyboard.current.f11Key.wasPressedThisFrame)
        {
            for (var i = 0; i < 10; i++)
            {
                var exp = GameManagerUtil.SurvivorsGameManager.PlayerEntity._experience;
                exp.AddExperience(exp.RequiredExperience);
                GameManagerUtil.SurvivorsGameManager.PlayerEntity._inventory.InventoryData.Rerolls = 100;
            }
        }

        _ui.PurgeState();
        foreach (var (_,stat) in _stats)
        {
            if (!stat.HasEffects) continue;
            _ui.Send(new StatState(stat.Name, stat.MaxDuration, stat.Effect.RemainingDuration));
        }

        foreach (var (_,shield) in _shields)
        {
            if (!shield.HasEffects) continue;
            var tempShield = shield.Effect;
            var tempShieldApplied =
                _health.TemporaryShields.Il2CppFirstOrDefault(x => x.Id == tempShield.TemporaryStatsId);
            _ui.Send(new ShieldState(shield.Name, shield.MaxDuration, tempShield.RemainingDuration, tempShieldApplied!.ShieldHealth));
        }

        _followers.Clear();
        foreach (var follower in GameManagerUtil.SurvivorsGameManager.PlayerEntity._followers.Followers)
        {
            var entity = follower.Follower;
            if (!_followers.TryAdd(entity.EntityName, 1))
                _followers[entity.EntityName]++;
        }

        foreach (var follower in _followers)
            _ui.Send(new MinionState(follower.Key[..^5], follower.Value));
    }

    public override void OnGUI()
    {
        if (_health == null) return;
        _ui.OnGUI();
    }
}