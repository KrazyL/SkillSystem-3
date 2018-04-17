public class Unit1 : Unit
{
    public Interactable DamageAbilityDamageDealer;

    AbilityBase _damageAbility;
    AbilityBase _dotAbility;

    protected override void InitInteractables()
    {
        base.InitInteractables();

        DamageAbilityDamageDealer.InitInteractable(this);
    }

    void InitDealDamageAbility()
    {
        _damageAbility = new AbilityBase
        {
            Caster = this
        };

        AbilityDamageAction dealDamageAction = new AbilityDamageAction("DealDamage", this, new DamageInfo(DamageType.Physical, -10))
        {
            TargetSelector = new ColliderBasedTargetSelector(this, DamageAbilityDamageDealer.InteractableCollider)
        };

        dealDamageAction.TargetSelector.SetTargetTypes(TargettedActionTargetType.Target);

        OnCollisionEnterAbilityEvent onCollisionEnterAbilityEvent = new OnCollisionEnterAbilityEvent(DamageAbilityDamageDealer.InteractableCollider, _damageAbility, dealDamageAction);
        onCollisionEnterAbilityEvent.TriggerActionList.Add(dealDamageAction);

        _damageAbility.Events.Add(onCollisionEnterAbilityEvent);
    }


    public void CastDamageAbility()
    {
        InitDealDamageAbility();

        _damageAbility.Cast();
    }

    void InitDOTAbility()
    {
        _dotAbility = new AbilityBase
        {
            Caster = this
        };

        AbilityDamageAction dealDamageAction = new AbilityDamageAction("DealDamage", this, new DamageInfo(DamageType.Physical, -10));
        dealDamageAction.TargetSelector = new SelectOtherTargetSelector(this, dealDamageAction);


        dealDamageAction.TargetSelector.SetTargetTypes(TargettedActionTargetType.Target);

        AbilityThinkerModifier dotThinker = new AbilityThinkerModifier("DOT Thinker", this, 3, 0.5f)
        {
            TargetSelector = new ColliderBasedTargetSelector(this, DamageAbilityDamageDealer.InteractableCollider)
        };

        dotThinker.TargetSelector.SetTargetTypes(TargettedActionTargetType.Target);

        OnThinkModifierEvent onThinkModifierEvent = new OnThinkModifierEvent(dotThinker, dotThinker);

        onThinkModifierEvent.TriggerActionList.Add(dealDamageAction);

        _dotAbility.Modifiers.Add(dotThinker);

        AbilityCreateThinkerAction createDOTThinkerAction = new AbilityCreateThinkerAction("DOT Thinker", this, dotThinker)
        {
            TargetSelector = new ColliderBasedTargetSelector(this, DamageAbilityDamageDealer.InteractableCollider)
        };

        createDOTThinkerAction.TargetSelector.SetTargetTypes(TargettedActionTargetType.Target);

        OnCastStartedAbilityEvent onCastStartedAbilityEvent = new OnCastStartedAbilityEvent(_dotAbility, null);

        onCastStartedAbilityEvent.TriggerActionList.Add(createDOTThinkerAction);

        _dotAbility.Events.Add(onCastStartedAbilityEvent);
    }

    public void CastDOTAbility()
    {
        InitDOTAbility();

        _dotAbility.Cast();
    }
}
