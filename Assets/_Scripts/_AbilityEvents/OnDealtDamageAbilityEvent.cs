public class OnDealtDamageAbilityEvent : AbilityEventBase
{
    IDamageDealerCombatUnit _parentUnit;

    public OnDealtDamageAbilityEvent(IDamageDealerCombatUnit parentUnit, AbilityActionBase bindedAction)
        : base(bindedAction)
    {
        _parentUnit = parentUnit;

        Bind();
    }

    protected override void Bind()
    {
        _parentUnit.OnDealtDamage += OnDealtDamage;
    }

    protected override void Unbind()
    {
        _parentUnit.OnDealtDamage -= OnDealtDamage;
        TriggerActionList.Clear();
    }

    private void OnDealtDamage(DamageInfo damageInfo, ICombatUnit other)
    {
        Unbind();

        Trigger();
    }
}
