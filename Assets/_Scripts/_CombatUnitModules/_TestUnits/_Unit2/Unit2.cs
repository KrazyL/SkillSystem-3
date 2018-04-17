public class Unit2 : Unit
{
    public Interactable Damagable;

    AbilityBase _poisonAbility;

    protected override void InitInteractables()
    {
        Damagable.InitInteractable(this);

        base.InitInteractables();
    }

    protected override void CastPassiveAbilities()
    {
        CastPoisonAbility();

        base.CastPassiveAbilities();
    }

    void CastPoisonAbility()
    {
        InitPoisonAbility();

        _poisonAbility.Cast();
    }

    void InitPoisonAbility()
    {
        _poisonAbility = new AbilityBase();
        _poisonAbility.Caster = this;

        AbilityDamageAction damageAction = new AbilityDamageAction(
            "Poison Damage",
            this,
            new DamageInfo(DamageType.Physical, -10));

        damageAction.TargetSelector = new SelectOtherTargetSelector(this, damageAction);

        AbilityThinkerModifier poisonThinker = new AbilityThinkerModifier(
            "Poison Thinker",
            this,
            5,
            1f);

        poisonThinker.TargetSelector = new SelectOtherTargetSelector(this, poisonThinker);

        OnThinkModifierEvent onThink = new OnThinkModifierEvent(poisonThinker, poisonThinker);
        onThink.TriggerActionList.Add(damageAction);

        _poisonAbility.Modifiers.Add(poisonThinker);

        AbilityCreateThinkerAction createPoisonThinkerAction = new AbilityCreateThinkerAction("Poison Thinker", this, poisonThinker);

        createPoisonThinkerAction.TargetSelector = new ActionEventTargetSelector(this, createPoisonThinkerAction);

        OnTakeDamageAbilityEvent onTakeDamage = new OnTakeDamageAbilityEvent(this, null, false);

        onTakeDamage.TriggerActionList.Add(createPoisonThinkerAction);

        _poisonAbility.Events.Add(onTakeDamage);
    }
}
