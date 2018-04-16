using System;

public interface IDamageDealerCombatUnit
{
    Action<DamageInfo> OnDealtDamage { get; set; }
    Action OnKilledEnemy { get; set; }

    DamageInfo DealDamage(DamageInfo damageInfo);
    void DealtDamage(DamageInfo damageInfo);
}
