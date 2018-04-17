using UnityEngine;
using System.Collections.Generic;

public enum InteractableTypeEnum
{
    None,
    DamageDealer,
    Damagable,
}

public class Interactable : MonoBehaviour
{
    public ICombatUnit ParentCombatUnit { get; private set; }

    public InteractableCollider InteractableCollider;

    public List<InteractableTypeEnum> InteractableTypes;

    public void InitInteractable(ICombatUnit parent)
    {
        ParentCombatUnit = parent;
    }
}
