using System.Collections.Generic;
using System.Linq;

public enum ValueModifierType
{
    Add,
    Mul,
}

public struct ResistanceInfo
{
    public ValueModifierType ModifierType { get; private set; }
    public DamageType DamageType { get; private set; }
    public float ModifierVal { get; private set; }

    public ResistanceInfo(
        ValueModifierType modifierType,
        DamageType damageType,
        float modifierVal) 
            : this()
    {
        ModifierType = modifierType;
        DamageType = damageType;
        ModifierVal = modifierVal;
    }

    public bool IsResistanceTo(DamageInfo damageInfo)
    {
        if (DamageType == damageInfo.DamageType)
            return true;

        return false;
    }

    public DamageInfo Apply(DamageInfo damageInfo)
    {
        if (damageInfo.Damage == 0)
            return damageInfo;

        if (ModifierType == ValueModifierType.Add)
            damageInfo.Damage += ModifierVal;
        else if (ModifierType == ValueModifierType.Mul)
            damageInfo.Damage *= ModifierVal;

        return damageInfo;
    }
}

public class ResistanceDamageStateDecorator : CombatUnitStateDecoratorBase,
    ITakeDamageDecorator
{
    public List<ResistanceInfo> ResistanceInfoList { get; private set; }

    public ResistanceDamageStateDecorator(ICombatUnit parentUnit, params ResistanceInfo[] resistanceArr) 
        : base(parentUnit)
    {
        ResistanceInfoList = resistanceArr.ToList();
    }

    public DamageInfo Decorate(DamageInfo damageInfo)
    {
        foreach(ResistanceInfo resInfo in ResistanceInfoList)
        {
            if (!resInfo.IsResistanceTo(damageInfo))
                continue;

            damageInfo = resInfo.Apply(damageInfo);
        }

        return damageInfo;
    }
}
