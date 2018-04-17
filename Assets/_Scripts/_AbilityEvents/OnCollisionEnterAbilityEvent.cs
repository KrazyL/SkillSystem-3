using System.Collections.Generic;

public class OnCollisionEnterAbilityEvent : AbilityEventBase
{
    AbilityBase _bindedAbility;

    InteractableCollider _targetInteractableCollider;

    public OnCollisionEnterAbilityEvent(InteractableCollider targetInteractableCollider, AbilityBase bindedAbility, AbilityActionBase bindedAction)
        : base(bindedAction)
    {
        _targetInteractableCollider = targetInteractableCollider;

        _bindedAbility = bindedAbility;

        Bind();
    }

    protected override void Bind()
    {
        _bindedAbility.OnAbilityExecuted += OnAbilityExecuted;
        _targetInteractableCollider.OnTargetEntered += OnTargetEntered;

        CheckInitialTriggger();
    }

    protected override void Unbind()
    {
        _bindedAbility.OnAbilityExecuted -= OnAbilityExecuted;
        _targetInteractableCollider.OnTargetEntered -= OnTargetEntered;

        TriggerActionList.Clear();
    }

    void CheckInitialTriggger()
    {
        List<ICombatUnit> targets = _targetInteractableCollider.Targets;

        foreach (ICombatUnit cUnit in targets)
            OnTargetEntered(cUnit);
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
