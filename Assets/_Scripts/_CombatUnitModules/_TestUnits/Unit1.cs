public class Unit1 : Unit
{
    public AbilityTargetCollectorBehaviour TargetCollectorBeh;

    AbilityBase _damageAbility;

    void InitDealDamageAbility()
    {
        _damageAbility = new AbilityBase
        {
            Caster = this
        };

        _damageAbility.Behaviours.Add(TargetCollectorBeh);

        #region Deal Damage Action
        AbilityDamageAction dealDamageAction = new AbilityDamageAction("DealDamage", this, new DamageInfo(DamageType.Physical, -10))
        {
            TargetSelector = new AbilityTargetSelector(this)
        };

        dealDamageAction.TargetSelector.SetTargetTypes(TargettedActionTargetType.Monster);

        //ElementalCondition elementalCondition = new ElementalCondition(this, true, ElementalType.Natural);
        //dealDamageAction.Conditions.Add(elementalCondition);

        OnCollisionEnterAbilityEvent onCollisionEnterAbilityEvent = new OnCollisionEnterAbilityEvent(this, _damageAbility);
        onCollisionEnterAbilityEvent.TriggerActionList.Add(dealDamageAction);

        //OnCastStartedAbilityEvent onCastStartedAbilityEvent = new OnCastStartedAbilityEvent(_damageAbility);

        //onCastStartedAbilityEvent.TriggerActionList.Add(dealDamageAction);
        #endregion

        #region Heal Action
        AbilityHealAction healSelfAction = new AbilityHealAction("HealSelf", this, 10)
        {
            TargetSelector = new AbilityTargetSelector(this)
        };

        healSelfAction.TargetSelector.SetTargetTypes(TargettedActionTargetType.Caster);

        OnDealtDamageAbilityEvent onDealtDamageAbilityEvent = new OnDealtDamageAbilityEvent(this);
        onDealtDamageAbilityEvent.TriggerActionList.Add(healSelfAction);
        #endregion

        _damageAbility.Events.Add(onCollisionEnterAbilityEvent);
        _damageAbility.Events.Add(onDealtDamageAbilityEvent);
    }


    public void CastDamageAbility()
    {
        InitDealDamageAbility();

        _damageAbility.Cast();
    }

}
