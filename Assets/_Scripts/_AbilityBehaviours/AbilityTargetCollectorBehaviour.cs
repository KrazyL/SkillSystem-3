using System;
using UnityEngine;

public class AbilityTargetCollectorBehaviour : AbilityBehaviourBase
{
    public Action<ICombatUnit> OnTargetEntered;
    public Action<ICombatUnit> OnTargetExited;

    void FireOnTargetEntered(ICombatUnit unit)
    {
        if (OnTargetEntered != null)
            OnTargetEntered(unit);
    }

    void FireOnTargetExited(ICombatUnit unit)
    {
        if (OnTargetExited != null)
            OnTargetExited(unit);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ICombatUnit target = other.gameObject.GetComponent<ICombatUnit>();

        if (target == null)
            return;

        FireOnTargetEntered(target);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ICombatUnit target = other.gameObject.GetComponent<ICombatUnit>();

        if (target == null)
            return;

        FireOnTargetExited(target);
    }
}
