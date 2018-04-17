using UnityEngine;
using System;
using System.Collections.Generic;

public class InteractableCollider : MonoBehaviour
{
    public Interactable ParentInteractable;

    public List<ICombatUnit> Targets { get; private set; }

    #region Events
    public Action<ICombatUnit> OnTargetEntered;

    void FireOnTargetEntered(ICombatUnit target)
    {
        if (OnTargetEntered != null)
            OnTargetEntered(target);
    }

    public Action<ICombatUnit> OnTargetExited;

    void FireOnTargetExited(ICombatUnit target)
    {
        if (OnTargetExited != null)
            OnTargetExited(target);
    }
    #endregion

    private void Awake()
    {
        Targets = new List<ICombatUnit>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable target = other.gameObject.GetComponent<Interactable>();

        if (target == null
            || target.ParentCombatUnit == ParentInteractable.ParentCombatUnit)
            return;

        Targets.Add(target.ParentCombatUnit);

        FireOnTargetEntered(target.ParentCombatUnit);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable target = other.gameObject.GetComponent<Interactable>();

        if (target == null
            || target.ParentCombatUnit == ParentInteractable.ParentCombatUnit)
            return;

        Targets.Remove(target.ParentCombatUnit);

        FireOnTargetExited(target.ParentCombatUnit);
    }
}
