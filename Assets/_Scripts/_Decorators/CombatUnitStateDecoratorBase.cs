using System.Collections.Generic;

public interface IStateDecorator
{
    bool CanDecorate();
}

public interface ITakeDamageDecorator
{
    DamageInfo Decorate(DamageInfo damageInfo);
}

public interface IDealDamageDecorator
{
    DamageInfo Decorate(DamageInfo damageInfo);
}

public abstract class CombatUnitStateDecoratorBase : IStateDecorator
{
    public ICombatUnit ParentUnit { get; private set; }

    public List<StateDecoratorConditionBase> Conditions { get; set; }

    public CombatUnitStateDecoratorBase(ICombatUnit parentUnit)
    {
        ParentUnit = parentUnit;

        Conditions = new List<StateDecoratorConditionBase>();
    }

    public bool CanDecorate()
    {
        foreach (StateDecoratorConditionBase cond in Conditions)
            if (!cond.IsSatisfied())
                return false;

        return true;
    }
}
