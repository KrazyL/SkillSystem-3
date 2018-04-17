using System.Collections.Generic;
using System.Linq;

public enum TargettedActionTargetType
{
    All,
    Caster,
    Target,
}

public interface IAbilityTargetSelector
{
    List<TargettedActionTargetType> TargetTypeList { get; }
    void SetTargetTypes(params TargettedActionTargetType[] targetTypeArr);
    List<ICombatUnit> GetTargets();
}

public abstract class TargetSelectorBase : IAbilityTargetSelector
{
    public List<TargettedActionTargetType> TargetTypeList { get; private set; }

    protected ICombatUnit _caster;
    protected AbilityActionBase _parentAciton;

    public TargetSelectorBase(ICombatUnit caster, AbilityActionBase parentAction)
    {
        _caster = caster;
        _parentAciton = parentAction;
    }

    public abstract List<ICombatUnit> GetTargets();

    public void SetTargetTypes(params TargettedActionTargetType[] targetTypeArr)
    {
        TargetTypeList = targetTypeArr.ToList();
    }
}
