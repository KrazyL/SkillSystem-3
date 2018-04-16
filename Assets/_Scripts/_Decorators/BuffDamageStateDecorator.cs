public class BuffDamageStateDecorator : CombatUnitStateDecoratorBase,
    IDealDamageDecorator
{
    public ValueModifierType ModifierType { get; private set; }
    public float ModifierVal { get; private set; }

    public BuffDamageStateDecorator(ICombatUnit parentUnit, ValueModifierType modifierType, float value) 
        : base(parentUnit)
    {
        ModifierType = modifierType;
        ModifierVal = value;
    }

    public DamageInfo Decorate(DamageInfo damageInfo)
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
