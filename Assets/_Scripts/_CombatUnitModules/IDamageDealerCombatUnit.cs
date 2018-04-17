using System;

public interface IDamageDealerCombatUnit
{
    Action<DamageInfo, ICombatUnit> OnDealtDamage { get; set; }
    Action<ICombatUnit> OnKilledEnemy { get; set; }

    DamageInfo DealDamage(DamageInfo damageInfo, ICombatUnit other);
    void DealtDamage(DamageInfo damageInfo, ICombatUnit other);
}
