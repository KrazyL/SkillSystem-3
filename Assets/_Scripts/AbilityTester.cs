using UnityEngine;

public class AbilityTester : MonoBehaviour
{
    public Unit1 CasterUnit;

    public Unit2 TargetUnit;

    ImmuneDamageStateDecorator _immuneDecorator;
    ResistanceDamageStateDecorator _resistanceDecorator;

    BuffDamageStateDecorator _buffDamageDecorator;

    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CasterUnit.CastDamageAbility();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            CasterUnit.CastDOTAbility();

        if (Input.GetKeyDown(KeyCode.Q))
            AddImmunity();
        if (Input.GetKeyDown(KeyCode.A))
            RemoveImmunity();

        if (Input.GetKeyDown(KeyCode.W))
            AddResistance();
        if (Input.GetKeyDown(KeyCode.S))
            RemoveResistance();

        if (Input.GetKeyDown(KeyCode.E))
            AddDamageBuff();
        if (Input.GetKeyDown(KeyCode.D))
            RemoveDamageBuff();
    }

    void AddImmunity()
    {
        _immuneDecorator = new ImmuneDamageStateDecorator(TargetUnit, DamageType.Physical);

        TargetUnit.AddStateDecorator(_immuneDecorator);
    }

    void RemoveImmunity()
    {
        TargetUnit.RemoveStateDecorator(_immuneDecorator);
    }

    void AddResistance()
    {
        _resistanceDecorator = new ResistanceDamageStateDecorator(TargetUnit, new ResistanceInfo(ValueModifierType.Add, DamageType.Physical, 5));

        TargetUnit.AddStateDecorator(_resistanceDecorator);
    }

    void RemoveResistance()
    {
        TargetUnit.RemoveStateDecorator(_resistanceDecorator);
    }

    void AddDamageBuff()
    {
        _buffDamageDecorator = new BuffDamageStateDecorator(CasterUnit, ValueModifierType.Mul, 1.2f);

        CasterUnit.AddStateDecorator(_buffDamageDecorator);
    }

    void RemoveDamageBuff()
    {
        CasterUnit.RemoveStateDecorator(_buffDamageDecorator);
    }
}
