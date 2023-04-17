using Il2Cpp;
using SoulstoneSurvivorsMods.BuffOverlay.Extensions;

namespace SoulstoneSurvivorsMods.BuffOverlay.Models;

public abstract class EffectWrapper<T> where T : SkillEffect
{
    protected readonly T _effect;
    protected readonly Skill _skill;

    public string Name => _skill.SkillName;
    public float MaxDuration { get; protected set; }
    
    public abstract TemporaryEffectInstance Effect { get; }
    public abstract bool HasEffects { get; }
    
    
    public EffectWrapper(T effect, Skill skill)
    {
        _effect = effect;
        _skill = skill;
    }
    
    public void UpdateDuration(float maxDuration)
    {
        MaxDuration = maxDuration;
    }

    public override int GetHashCode() => _effect.GetHashCode();
}

public sealed class TemporaryShieldWrapper : EffectWrapper<TemporaryShieldOnActivationEffect>
{
    public override bool HasEffects => _effect.Instances.Count > 0;
    public override TemporaryEffectInstance Effect => _effect.Instances.Il2CppFirst();

    public TemporaryShieldWrapper(TemporaryShieldOnActivationEffect effect, Skill skill) : base(effect, skill)
    {
        MaxDuration = effect.Duration;
    }
}

public sealed class TemporaryStatsWrapper : EffectWrapper<TemporaryStatsOnActivationEffect>
{
    public override bool HasEffects => _effect.Instances.Count > 0;
    public override TemporaryEffectInstance Effect => _effect.Instances.Il2CppFirst();
    
    public TemporaryStatsWrapper(TemporaryStatsOnActivationEffect effect, Skill skill) : base(effect, skill)
    {
        MaxDuration = effect.Duration;
    }
}