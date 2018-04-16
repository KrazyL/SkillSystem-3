using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TargettedActionTargetType
{
    All,
    Caster,
    Monster,
}

public interface IAbilityTargetSelector
{
    List<TargettedActionTargetType> TargetTypeList { get;}
    void SetTargetTypes(params TargettedActionTargetType[] targetTypeArr);
    List<ICombatUnit> GetTargets();
}

public class AbilityTargetSelector : IAbilityTargetSelector
{
    public List<TargettedActionTargetType> TargetTypeList { get; private set; }

    ICombatUnit _caster;

    public AbilityTargetSelector(ICombatUnit caster)
    {
        _caster = caster;
    }

    public List<ICombatUnit> GetTargets()
    {
        List<ICombatUnit> targets = new List<ICombatUnit>();

        if(TargetTypeList.Contains(TargettedActionTargetType.Caster))
        {
            targets.Add(_caster);
            return targets;
        }

        targets.Add(MonsterController.Instance.EnemyUnit);
        return targets;
    }

    public void SetTargetTypes(params TargettedActionTargetType[] targetTypeArr)
    {
        TargetTypeList = targetTypeArr.ToList();
    }
}
