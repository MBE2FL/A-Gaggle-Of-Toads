using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageData : ScriptableObject
{
    [SerializeField]
    byte _damage = 50;
    [SerializeField]
    float _attackSpeed = 2.0f;



    public byte Damage
    {
        get 
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    public float AttackSpeed
    {
        get
        {
            return _attackSpeed;
        }
        set
        {
            _attackSpeed = value;
        }
    }
}
