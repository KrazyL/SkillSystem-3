using System;
using System.Collections;
using UnityEngine;

public class AbilityThinkerModifier : AbilityModifierBase
{
    public float Duration { get; private set; }
    public float ThinkInterval { get; private set; }

    public Action OnThink;

    IEnumerator _thinkRoutine;

    void FireOnThink()
    {
        if (OnThink != null)
            OnThink();
    }

    public AbilityThinkerModifier(string name, ICombatUnit caster, float duration, float thinkInterval) 
        : base(name, caster)
    {
        Duration = duration;
        ThinkInterval = thinkInterval;
    }

    protected override void Invoke()
    {
        StartThinkProgress();
    }

    void StartThinkProgress()
    {
        StopThinkProgress();

        _thinkRoutine = ThinkProgress();
        Caster.Mono.StartCoroutine(_thinkRoutine);
    }

    void StopThinkProgress()
    {
        if (_thinkRoutine != null)
            Caster.Mono.StopCoroutine(_thinkRoutine);
    }

    IEnumerator ThinkProgress()
    {
        float remDuration = Duration;

        while(remDuration > 0)
        {
            Think();

            yield return new WaitForSeconds(ThinkInterval);

            remDuration -= ThinkInterval;
        }
    }

    void Think()
    {
        FireOnThink();
    }
}
