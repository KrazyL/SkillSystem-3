using System.Collections.Generic;

public class CasterTargetSelector : TargetSelectorBase
{
    public CasterTargetSelector(ICombatUnit caster, AbilityActionBase bindedAction) 
        : base(caster, bindedAction)
    {
    }

    public override List<ICombatUnit> GetTargets()
    {
        List<ICombatUnit> targetList = new List<ICombatUnit>();

        targetList.Add(_caster);

        return targetList;
    }
}
