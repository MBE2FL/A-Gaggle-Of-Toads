using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New EnemyDamageData", menuName = "Scriptable Objects/EnemyDamageData", order = 51)]
public class EnemyDamageData : ScriptableObject
{
    [SerializeField]
    byte _damage = 50;
    [SerializeField]
    float _attackSpeed = 2.0f;

    [SerializeField]
    float _attackArc = 60.0f;
    [SerializeField]
    GameObject _throwObj;
    [SerializeField]
    Vector2 _throwForce;
    [SerializeField]
    Vector2 _throwForceRange;



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

    public float AttackArc
    {
        get
        {
            return _attackArc;
        }
        set
        {
            _attackArc = value;
        }
    }

    public GameObject ThrowObj
    {
        get
        {
            return _throwObj;
        }
        set
        {
            _throwObj = value;
        }
    }

    public Vector2 ThrowForce
    {
        get
        {
            return _throwForce;
        }
        set
        {
            _throwForce = value;
        }
    }

    public Vector2 ThrowForceRange
    {
        get
        {
            return _throwForceRange;
        }
        set
        {
            _throwForceRange = value;
        }
    }
}
