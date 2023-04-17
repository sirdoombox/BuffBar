using BuffBar.Extensions;
using Il2Cpp;

namespace BuffBar.Models;

public sealed class TemporaryStatsWrapper : EffectWrapper<TemporaryStatsOnActivationEffect>
{
    public override bool HasEffects => SkillEffect.Instances.Count > 0;
    public override TemporaryEffectInstance Effect => SkillEffect.Instances.Il2CppFirst();

    public TemporaryStatsWrapper(TemporaryStatsOnActivationEffect skillEffect, Skill applyingSkill)
        : base(skillEffect, applyingSkill) =>
        MaxDuration = skillEffect.Duration;
}