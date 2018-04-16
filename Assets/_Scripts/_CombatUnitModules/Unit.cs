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

    AbilityBase _dotAbility;

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
        StateDecorators = new List<CombatUnitStateDecoratorBase>();

        Flags = GetComponents<CombatUnitFlagBase>().ToList();
    }

    #region IHealthOwner
    public DamageInfo TakeDamage(DamageInfo damageInfo)
    {
        damageInfo = ((ITakeDamageDecorator)this).Decorate(damageInfo);

        damageInfo.Damage = Health.AddHealth(damageInfo.Damage);

        Debug.Log("<color=red>" + gameObject.name + " Took Damage: " + damageInfo.Damage + " Cur Health: " + Health.CurValue + "</color>");

        if (damageInfo.Damage == 0)
            return damageInfo;

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

    #region Ability Define Region


    void InitDOTAbility()
    {
        _dotAbility = new AbilityBase
        {
            Caster = this
        };

        AbilityDamageAction dealDamageAction = new AbilityDamageAction("DealDamage", this, new DamageInfo(DamageType.Physical, -10))
        {
            TargetSelector = new AbilityTargetSelector(this)
        };

        dealDamageAction.TargetSelector.SetTargetTypes(TargettedActionTargetType.Monster);

        AbilityThinkerModifier dotThinker = new AbilityThinkerModifier("DOT Thinker", this, 3, 0.5f);

        OnThinkModifierEvent onThinkModifierEvent = new OnThinkModifierEvent(dotThinker);

        onThinkModifierEvent.TriggerActionList.Add(dealDamageAction);

        _dotAbility.Modifiers.Add(dotThinker);

        AbilityCreateThinkerAction createDOTThinkerAction = new AbilityCreateThinkerAction("DOT Thinker", this, dotThinker);

        OnCastStartedAbilityEvent onCastStartedAbilityEvent = new OnCastStartedAbilityEvent(_dotAbility);

        onCastStartedAbilityEvent.TriggerActionList.Add(createDOTThinkerAction);

        _dotAbility.Events.Add(onCastStartedAbilityEvent);
    }

    public void CastDOTAbility()
    {
        InitDOTAbility();

        _dotAbility.Cast();
    }
    #endregion
}
