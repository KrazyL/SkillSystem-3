public abstract class EventConditionBase
{
    public ICombatUnit Caster { get; private set; }

    public EventConditionBase(ICombatUnit caster)
    {
        Caster = caster;
    }

    public abstract bool IsSatisfied(ICombatUnit target);

}
