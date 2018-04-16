using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public interface ICombatUnit
{
    MonoBehaviour Mono { get; }

    Action<float> OnSpentStamina { get; set; }

    List<CombatUnitStateDecoratorBase> StateDecorators { get; }

    List<CombatUnitFlagBase> Flags { get; }

    ICombatUnit AddStateDecorator(CombatUnitStateDecoratorBase stateDecorator);
    ICombatUnit RemoveStateDecorator(CombatUnitStateDecoratorBase stateDecorator);
}

public static class ICombatUnitExtensions
{
    public static TFlag GetFlag<TFlag>(this ICombatUnit unit) 
        where TFlag : CombatUnitFlagBase
    {
        return (TFlag)unit.Flags.SingleOrDefault(val => val is TFlag);
    }
}
