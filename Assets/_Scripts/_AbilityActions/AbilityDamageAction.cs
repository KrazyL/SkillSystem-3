public enum DamageType
{
    None,
    Physical,

}

public struct DamageInfo
{
    public DamageType DamageType;
    public float Damage;

    public DamageInfo(DamageType damageType, float damage)
    {
        DamageType = damageType;
        Damage = damage;
    }
}

public class AbilityDamageAction : AbilityActionBase
{
    public DamageInfo DamageInfo { get; private set; }

    public AbilityDamageAction(string name, ICombatUnit caster, DamageInfo damageInfo) 
        : base(name, caster)
    {
        DamageInfo = damageInfo;
    }

    protected override void Invoke()
    {
        DealDamageToTargets();
    }

    void DealDamageToTargets()
    {
        TargetUnits.ForEach(val => DealDamageToTarget(val));
    }

    void DealDamageToTarget(ICombatUnit combatUnit)
    {
        DamageInfo decoratedDamageInfo = ((IDamageDealerCombatUnit)Caster).DealDamage(DamageInfo);

        DamageInfo dealtDamageInfo = ((IHealthOwnerCombatUnit)combatUnit).TakeDamage(decoratedDamageInfo);

        if (dealtDamageInfo.Damage == 0)
            return;

        ((IDamageDealerCombatUnit)Caster).DealtDamage(dealtDamageInfo);

        FireOnActionEnded();
    }
}
