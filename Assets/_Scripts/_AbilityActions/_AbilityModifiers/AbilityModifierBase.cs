using System;
using System.Collections.Generic;

public abstract class AbilityModifierBase : AbilityActionBase
{
    public List<AbilityEventBase> Events { get; set; }

    public Action OnCreated;
    public Action OnDestroy;

    void FireOnCreated()
    {
        if (OnCreated != null)
            OnCreated();
    }

    public AbilityModifierBase(string name, ICombatUnit caster)
        : base(name, caster)
    {
        Events = new List<AbilityEventBase>();
    }

    public void CreateModifier()
    {
        FireOnCreated();
    }
}
