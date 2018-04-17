using System.Collections.Generic;

public class ActionEventTargetSelector : TargetSelectorBase
{
    public ActionEventTargetSelector(ICombatUnit caster, AbilityActionBase parentAction) 
        : base(caster, parentAction)
    {
    }

    public override List<ICombatUnit> GetTargets()
    {
        ICombatUnitTriggeredEvent combatUnitTriggeredEvent = _parentAciton.ParentEvent as ICombatUnitTriggeredEvent;

        if (combatUnitTriggeredEvent == null)
            return new List<ICombatUnit>();

        List<ICombatUnit> targetList = new List<ICombatUnit>();
        targetList.Add(combatUnitTriggeredEvent.Other);

        return targetList;
    }
}
