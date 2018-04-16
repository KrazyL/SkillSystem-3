using UnityEngine;

public abstract class StateDecoratorConditionBase : MonoBehaviour
{
    public ICombatUnit Parent { get; private set; }

    public StateDecoratorConditionBase(ICombatUnit parent)
    {
        Parent = parent;
    }

    public abstract bool IsSatisfied();
}
