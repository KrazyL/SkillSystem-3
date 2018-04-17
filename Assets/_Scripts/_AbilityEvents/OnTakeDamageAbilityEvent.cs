public class OnTakeDamageAbilityEvent : AbilityEventBase, ICombatUnitTriggeredEvent
{
    IHealthOwnerCombatUnit _parentUnit;

    public ICombatUnit Other { get; private set; }

    public OnTakeDamageAbilityEvent(IHealthOwnerCombatUnit parentUnit, AbilityActionBase bindedAction, bool triggerOnce)
        : base(bindedAction, triggerOnce)
    {
        _parentUnit = parentUnit;

        Bind();
    }


    protected override void Bind()
    {
        _parentUnit.OnTookDamage += OnTakeDamage;
    }

    protected override void Unbind()
    {
        _parentUnit.OnTookDamage -= OnTakeDamage;

        TriggerActionList.Clear();
    }

    private void OnTakeDamage(DamageInfo damageInfo, ICombatUnit other)
    {
        Other = other;

        Trigger();

        if(_triggerOnce)
            Unbind();
    }
}
