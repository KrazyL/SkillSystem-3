public class OnTakeDamageAbilityEvent : AbilityEventBase
{
    IHealthOwnerCombatUnit _parentUnit;

    public OnTakeDamageAbilityEvent(IHealthOwnerCombatUnit parentUnit, AbilityActionBase bindedAction)
        : base(bindedAction)
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

    private void OnTakeDamage(DamageInfo damageInfo)
    {
        Unbind();

        Trigger();
    }

}
