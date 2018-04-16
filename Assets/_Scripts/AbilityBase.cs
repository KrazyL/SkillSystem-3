using System.Collections.Generic;
using System;
using System.Linq;

public class AbilityBase
{
    public ICombatUnit Caster;

    public List<AbilityBehaviourBase> Behaviours { get; set; }

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
        Behaviours = new List<AbilityBehaviourBase>();
        Events = new List<AbilityEventBase>();
        Modifiers = new List<AbilityModifierBase>();
    }

    public void Cast()
    {
        FireOnCastStarted();
    }

    public TBehaviour GetBehaviour<TBehaviour>()
        where TBehaviour : AbilityBehaviourBase
        {
            return (TBehaviour)Behaviours.SingleOrDefault(val => val is TBehaviour);
        }
}
