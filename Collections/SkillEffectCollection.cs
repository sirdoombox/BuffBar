using System.Collections;
using BuffBar.Models;
using Il2Cpp;

namespace BuffBar.Collections;

public class SkillEffectCollection<TWrapper, TSkillEffect> : IEnumerable<TWrapper>
    where TWrapper : EffectWrapper<TSkillEffect>
    where TSkillEffect : SkillEffect
{
    private readonly Dictionary<int, TWrapper> _effects = new();

    public void UpdateDuration(TSkillEffect effect, float duration) => _effects[effect.GetHashCode()].UpdateDuration(duration);

    public void Add(TWrapper effect) => _effects.Add(effect.GetHashCode(), effect);

    public IEnumerator<TWrapper> GetEnumerator() => _effects.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Clear() => _effects.Clear();
}