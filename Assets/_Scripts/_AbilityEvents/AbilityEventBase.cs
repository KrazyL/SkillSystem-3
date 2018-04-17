using System.Collections.Generic;

public abstract class AbilityEventBase
{
    public List<AbilityActionBase> TriggerActionList;

    AbilityActionBase _bindedAction;

    protected bool _triggerOnce;

    public AbilityEventBase(AbilityActionBase bindedAction, bool triggerOnce = true)
    {
        _bindedAction = bindedAction;

        TriggerActionList = new List<AbilityActionBase>();
    }

    protected abstract void Bind();
    protected abstract void Unbind();

    protected void Trigger()
    {
        TriggerActionList.ForEach(val => val.CheckAndInvoke(_bindedAction, this));
    }
}
