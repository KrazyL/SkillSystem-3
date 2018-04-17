using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour, ICombatUnit,
    IDamageDealerCombatUnit, IHealthOwnerCombatUnit,
    ITakeDamageDecorator, IDealDamageDecorator
{
    public Health Health;

    public MonoBehaviour Mono { get { return this; } }

    public List<CombatUnitStateDecoratorBase> StateDecorators { get; private set; }
    public List<CombatUnitFlagBase> Flags { get; private set; }

    public Action<float> OnSpentStamina { get; set; }

    public Action<DamageInfo> OnTookDamage { get; set; }
    public Action<float> OnHealthGained { get; set; }

    public Action OnKilledEnemy { get; set; }
    public Action<DamageInfo> OnDealtDamage { get; set; }

    void FireOnTookDamage(DamageInfo damageInfo)
    {
        if (OnTookDamage != null)
            OnTookDamage(damageInfo);
    }

    void FireOnDealtDamage(DamageInfo damageInfo)
    {
        if (OnDealtDamage != null)
            OnDealtDamage(damageInfo);
    }

    private void Awake()
    {
        InitInteractables();

        StateDecorators = new List<CombatUnitStateDecoratorBase>();

        Flags = GetComponents<CombatUnitFlagBase>().ToList();
    }

    protected virtual void InitInteractables()
    {

    }

    #region IHealthOwner
    public DamageInfo TakeDamage(DamageInfo damageInfo)
    {
        damageInfo = ((ITakeDamageDecorator)this).Decorate(damageInfo);

        damageInfo.Damage = Health.AddHealth(damageInfo.Damage);

        if (damageInfo.Damage == 0)
            return damageInfo;

        Debug.Log("<color=red>" + gameObject.name + " Took Damage: " + damageInfo.Damage + " Cur Health: " + Health.CurValue + "</color>");

        FireOnTookDamage(damageInfo);

        return damageInfo;
    }

    public void Heal(float amount)
    {
        float healAmount = Health.AddHealth(amount);

        Debug.Log("<color=green>" + gameObject.name + " Healed: " + healAmount + " Cur Health: " + Health.CurValue + "</color>");
    }
    #endregion

    #region IDamageDealer
    public DamageInfo DealDamage(DamageInfo damageInfo)
    {
        damageInfo = ((IDealDamageDecorator)this).Decorate(damageInfo);

        return damageInfo;
    }

    public void DealtDamage(DamageInfo damageInfo)
    {
        FireOnDealtDamage(damageInfo);
    }
    #endregion

    #region Decorator Region
    public ICombatUnit AddStateDecorator(CombatUnitStateDecoratorBase stateDecorator)
    {
        StateDecorators.Add(stateDecorator);

        Debug.Log("<color=green>" + stateDecorator.GetType() + " added to " + gameObject.name + "</color>");

        return this;
    }

    public ICombatUnit RemoveStateDecorator(CombatUnitStateDecoratorBase stateDecorator)
    {
        StateDecorators.Remove(stateDecorator);

        Debug.Log("<color=red>" + stateDecorator.GetType() + " removed from " + gameObject.name + "</color>");

        return this;
    }
    #endregion

    #region Decoration Region
    DamageInfo ITakeDamageDecorator.Decorate(DamageInfo damageInfo)
    {
        foreach (var decorator in StateDecorators)
        {
            ITakeDamageDecorator damageDecorator = decorator as ITakeDamageDecorator;

            if (damageDecorator == null
                || !((IStateDecorator)damageDecorator).CanDecorate())
                continue;

            damageInfo = damageDecorator.Decorate(damageInfo);
        }

        return damageInfo;
    }

    DamageInfo IDealDamageDecorator.Decorate(DamageInfo damageInfo)
    {
        foreach (var decorator in StateDecorators)
        {
            IDealDamageDecorator damageDecorator = decorator as IDealDamageDecorator;

            if (damageDecorator == null
                || !((IStateDecorator)damageDecorator).CanDecorate())
                continue;

            damageInfo = damageDecorator.Decorate(damageInfo);
        }

        return damageInfo;
    }
    #endregion

}
