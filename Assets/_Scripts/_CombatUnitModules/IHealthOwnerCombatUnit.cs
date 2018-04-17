using System;

public interface IHealthOwnerCombatUnit
{
    Action<DamageInfo, ICombatUnit> OnTookDamage { get; set; }
    Action<float> OnHealthGained { get; set; }

    DamageInfo TakeDamage(DamageInfo damageInfo, ICombatUnit other);
    void Heal(float amount);
}
