using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    static MonsterController _instance;

    public static MonsterController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MonsterController>();

            return _instance;
        }
    }

    public Unit EnemyUnit;

    private void OnDestroy()
    {
        _instance = null;
    }

}
