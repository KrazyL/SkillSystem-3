using System;

public interface IHealthOwnerCombatUnit
{
    Action<DamageInfo> OnTookDamage { get; set; }
    Action<float> OnHealthGained { get; set; }

    DamageInfo TakeDamage(DamageInfo damageInfo);
    void Heal(float amount);
}
