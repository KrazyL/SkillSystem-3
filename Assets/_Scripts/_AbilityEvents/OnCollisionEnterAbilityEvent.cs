public class OnCollisionEnterAbilityEvent : AbilityEventBase
{
    ICombatUnit _parentUnit;

    AbilityBase _bindedAbility;

    AbilityTargetCollectorBehaviour _targetCollectorBehaviour;

    public OnCollisionEnterAbilityEvent(ICombatUnit parentUnit, AbilityBase bindedAbility)
    {
        _parentUnit = parentUnit;
        _bindedAbility = bindedAbility;

        _targetCollectorBehaviour = _bindedAbility.GetBehaviour<AbilityTargetCollectorBehaviour>();

        Bind();
    }

    protected override void Bind()
    {
        _bindedAbility.OnAbilityExecuted += OnAbilityExecuted;
        _targetCollectorBehaviour.OnTargetEntered += OnTargetEntered;
    }

    protected override void Unbind()
    {
        _bindedAbility.OnAbilityExecuted -= OnAbilityExecuted;
        _targetCollectorBehaviour.OnTargetEntered -= OnTargetEntered;

        TriggerActionList.Clear();
    }

    private void OnTargetEntered(ICombatUnit unit)
    {
        Trigger();
    }

    void OnAbilityExecuted()
    {
        Unbind();
    }
}
