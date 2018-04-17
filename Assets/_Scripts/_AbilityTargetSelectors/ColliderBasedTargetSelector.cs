using System.Collections.Generic;
using System.Linq;

public class ColliderBasedTargetSelector : IAbilityTargetSelector
{
    public List<TargettedActionTargetType> TargetTypeList { get; private set; }

    ICombatUnit _caster;

    InteractableCollider _targetCollider;

    public ColliderBasedTargetSelector(ICombatUnit caster, InteractableCollider targetCollider)
    {
        _caster = caster;
        _targetCollider = targetCollider;
    }

    public List<ICombatUnit> GetTargets()
    {
        List<ICombatUnit> targetList = new List<ICombatUnit>();

        if (TargetTypeList.Contains(TargettedActionTargetType.Caster))
            targetList.Add(_caster);

        if (TargetTypeList.Contains(TargettedActionTargetType.Target))
            targetList.AddRange(_targetCollider.Targets);

        return targetList;
    }

    public void SetTargetTypes(params TargettedActionTargetType[] targetTypeArr)
    {
        TargetTypeList = targetTypeArr.ToList();
    }
}
