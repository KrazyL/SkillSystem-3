using System.Collections.Generic;
using System.Linq;

public class ElementalCondition : EventConditionBase
{
    public bool DoesInclude { get; private set; }
    public List<ElementalType> ElementalTypes { get; private set; }

    public ElementalCondition(ICombatUnit caster, bool doesInclude, params ElementalType[] elementalTypes)
        : base(caster)
    {
        DoesInclude = doesInclude;
        ElementalTypes = elementalTypes.ToList();
    }

    public override bool IsSatisfied(ICombatUnit target)
    {
        ElementalFlag flag = target.GetFlag<ElementalFlag>();

        if (ElementalTypes.Contains(flag.ElementalType))
            return true;

        return false;
    }
}
