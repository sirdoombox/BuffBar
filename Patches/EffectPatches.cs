﻿using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using SoulstoneSurvivorsMods.BuffOverlay.Models;

namespace SoulstoneSurvivorsMods.BuffOverlay.Patches;

[HarmonyPatch]
public static class EffectPatches
{
    [HarmonyPatch(typeof(TemporaryShieldOnActivationEffect), nameof(TemporaryShieldOnActivationEffect.Setup))]
    [HarmonyPostfix]
    public static void Setup(TemporaryShieldOnActivationEffect __instance, Skill skill)
    {
        if (string.IsNullOrWhiteSpace(skill.SkillName)) return;
        Melon<BuffOverlayMod>.Instance.AddEffect(__instance, skill);
    }

    [HarmonyPatch(typeof(TemporaryStatsOnActivationEffect), nameof(TemporaryStatsOnActivationEffect.Setup))]
    [HarmonyPostfix]
    public static void Setup(TemporaryStatsOnActivationEffect __instance, Skill skill)
    {
        if (string.IsNullOrWhiteSpace(skill.SkillName)) return;
        Melon<BuffOverlayMod>.Instance.AddEffect(__instance, skill);
    }

    [HarmonyPatch(typeof(TemporaryStatsOnActivationEffect), nameof(TemporaryStatsOnActivationEffect.DoApplyEffects))]
    [HarmonyPostfix]
    public static void DoApplyEffects(TemporaryStatsOnActivationEffect __instance, SkillTargetToProcessData target)
    {
        if (!target.TargetEntity.IsPlayer()) return;
        var durationModifier = target.EffectModifier.Modifiers.Modifiers.BuffDurationModifier;
        var modifiedDuration = __instance.Duration + (__instance.Duration * durationModifier);
        Melon<BuffOverlayMod>.Instance.UpdateDurationOfEffect(__instance, modifiedDuration);
    }
    
    [HarmonyPatch(typeof(TemporaryShieldOnActivationEffect), nameof(TemporaryShieldOnActivationEffect.DoApplyEffects))]
    [HarmonyPostfix]
    public static void DoApplyEffects(TemporaryShieldOnActivationEffect __instance, SkillTargetToProcessData target)
    {
        if (!target.TargetEntity.IsPlayer()) return;
        var durationModifier = target.EffectModifier.Modifiers.Modifiers.BuffDurationModifier;
        var modifiedDuration = __instance.Duration + (__instance.Duration * durationModifier);
        Melon<BuffOverlayMod>.Instance.UpdateDurationOfEffect(__instance, modifiedDuration);
    }
}