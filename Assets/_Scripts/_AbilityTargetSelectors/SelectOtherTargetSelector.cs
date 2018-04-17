using System.Collections.Generic;

public class SelectOtherTargetSelector : TargetSelectorBase
{
    public SelectOtherTargetSelector(ICombatUnit caster, AbilityActionBase parentAction) 
        : base(caster, parentAction)
    {
    }

    public override List<ICombatUnit> GetTargets()
    {
        return _parentAciton.ParentAction.TargetUnits;
    }
}
