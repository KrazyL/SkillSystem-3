using System.Collections.Generic;
using System.Linq;

public class ImmuneDamageStateDecorator : CombatUnitStateDecoratorBase,
    ITakeDamageDecorator
{
    public List<DamageType> ImmuneDamageTypes { get; private set; }

    public ImmuneDamageStateDecorator(ICombatUnit parentUnit, params DamageType[] damageTypeArr) 
        : base(parentUnit)
    {
        ImmuneDamageTypes = damageTypeArr.ToList();
    }

    public DamageInfo Decorate(DamageInfo damageInfo)
    {
        if (!ImmuneDamageTypes.Contains(damageInfo.DamageType))
            return damageInfo;

        damageInfo.Damage = 0;

        return damageInfo;
    }
}
