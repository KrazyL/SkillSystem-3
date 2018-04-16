using System.Collections.Generic;
using System;

public abstract class AbilityEventBase
{
    public List<AbilityActionBase> TriggerActionList;

    public AbilityEventBase()
    {
        TriggerActionList = new List<AbilityActionBase>();
    }

    protected abstract void Bind();
    protected abstract void Unbind();

    protected void Trigger()
    {
        TriggerActionList.ForEach(val => val.CheckAndInvoke());
    }
}
