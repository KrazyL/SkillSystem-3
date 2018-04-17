using System;
using System.Collections.Generic;

public abstract class AbilityActionBase
{
    public string ActionName;

    public IAbilityTargetSelector TargetSelector;
    public List<ICombatUnit> TargetUnits;

    public ICombatUnit Caster { get; private set; }

    public AbilityActionBase ParentAction { get; protected set; }

    #region Events
    public Action<AbilityActionBase> OnActionStarted;

    void FireOnActionStarted()
    {
        if (OnActionStarted != null)
            OnActionStarted(this);
    }

    public Action<AbilityActionBase> OnActionEnded;

    protected void FireOnActionEnded()
    {
        if (OnActionEnded != null)
            OnActionEnded(this);
    }
    #endregion

    public List<EventConditionBase> Conditions { get; set; }

    public AbilityActionBase(string name, ICombatUnit caster)
    {
        Caster = caster;

        Conditions = new List<EventConditionBase>();
    }

    public void CheckAndInvoke(AbilityActionBase parentAction)
    {
        ParentAction = parentAction;

        SetTargetUnits();

        foreach (ICombatUnit target in TargetUnits)
        {
            bool condsSatisfied = true;

            foreach (EventConditionBase cond in Conditions)
                if (!cond.IsSatisfied(target))
                {
                    condsSatisfied = false;
                    break;
                }

            if (condsSatisfied)
            {
                FireOnActionStarted();
                Invoke();
            }
        }
    }

    protected abstract void Invoke();

    protected void SetTargetUnits()
    {
        TargetUnits = TargetSelector.GetTargets();
    }
}
