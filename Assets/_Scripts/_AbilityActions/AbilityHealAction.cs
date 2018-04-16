public class AbilityHealAction : AbilityActionBase
{
    public float HealtAmount { get; private set; }

    public AbilityHealAction(string name, ICombatUnit caster, float healthAmount) 
        : base(name, caster)
    {
        HealtAmount = healthAmount;
    }

    protected override void Invoke()
    {
        HealTargets();
    }

    void HealTargets()
    {
        TargetUnits.ForEach(val => HealTarget(val));
    }

    void HealTarget(ICombatUnit combatUnit)
    {
        ((IHealthOwnerCombatUnit)combatUnit).Heal(HealtAmount);

        FireOnActionEnded();
    }
}
