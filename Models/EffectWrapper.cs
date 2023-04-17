using Il2Cpp;

namespace BuffBar.Models;

public abstract class EffectWrapper<T> where T : SkillEffect
{
    public string Name => ApplyingSkill.SkillName;
    public float MaxDuration { get; protected set; }

    public abstract TemporaryEffectInstance Effect { get; }
    public abstract bool HasEffects { get; }

    protected readonly T SkillEffect;
    protected readonly Skill ApplyingSkill;

    protected EffectWrapper(T skillEffect, Skill applyingSkill)
    {
        SkillEffect = skillEffect;
        ApplyingSkill = applyingSkill;
    }

    public void UpdateDuration(float maxDuration) => MaxDuration = maxDuration;

    public override int GetHashCode() => SkillEffect.GetHashCode();
}