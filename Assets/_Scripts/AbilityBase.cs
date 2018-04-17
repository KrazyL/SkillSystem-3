using System.Collections.Generic;
using System;

public class AbilityBase
{
    public ICombatUnit Caster;

    public List<AbilityEventBase> Events { get; set; }

    public List<AbilityModifierBase> Modifiers { get; set; }

    public Action OnCastStarted;

    void FireOnCastStarted()
    {
        if (OnCastStarted != null)
            OnCastStarted();
    }

    public Action OnAbilityExecuted;

    void FireOnAbilityExecuted()
    {
        if (OnAbilityExecuted != null)
            OnAbilityExecuted();
    }

    public AbilityBase()
    {
        Events = new List<AbilityEventBase>();
        Modifiers = new List<AbilityModifierBase>();
    }

    public void Cast()
    {
        FireOnCastStarted();
    }
}
