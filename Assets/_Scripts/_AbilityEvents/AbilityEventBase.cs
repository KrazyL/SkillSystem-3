using System.Collections.Generic;
using System;

public abstract class AbilityEventBase
{
    public List<AbilityActionBase> TriggerActionList;

    AbilityActionBase _bindedAction;

    public AbilityEventBase(AbilityActionBase bindedAction)
    {
        _bindedAction = bindedAction;

        TriggerActionList = new List<AbilityActionBase>();
    }

    protected abstract void Bind();
    protected abstract void Unbind();

    protected void Trigger()
    {
        TriggerActionList.ForEach(val => val.CheckAndInvoke(_bindedAction));
    }
}
