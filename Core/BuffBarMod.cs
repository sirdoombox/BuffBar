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
            _ui = new OverlayUI(font);
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
        {
            var name = follower.Key;
            if (name.EndsWith("Title"))
                name = name[..^5];
            _ui.Send(new MinionState(name, follower.Value));
        }
    }

    public override void OnGUI()
    {
        if (_health == null) return;
        _ui.OnGUI();
    }
}