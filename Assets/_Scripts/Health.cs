using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxValue;

    float _curValue;

    public float CurValue
    {
        get
        {
            return _curValue;
        }
        set
        {
            _curValue = value;

            FireOnHealthUpdated();
        }
    }

    public Action<float> OnHealthUpdated;

    void FireOnHealthUpdated()
    {
        if (OnHealthUpdated != null)
            OnHealthUpdated(CurValue);
    }

    private void Awake()
    {
        InitHealth();
    }

    void InitHealth()
    {
        CurValue = MaxValue;
    }

    public float AddHealth(float amount)
    {
        if (amount < 0
            && Math.Abs(amount) > CurValue)
            amount = -CurValue;

        if (amount == 0)
            return amount;

        CurValue += amount;

        return amount;
    }
}
