using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttack : MonoBehaviour
{
    [SerializeField]
    EnemyDamageData _enemyDamageData;
    float _attackTimer;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer > _enemyDamageData.AttackSpeed)
        {
            _attackTimer = 0;


            // Compute the ray direction to throw the obj.
            float attackAngle = Random.Range(0.0f, _enemyDamageData.AttackArc) + _enemyDamageData.AttackArc;
            Vector3 throwRay = Quaternion.Euler(new Vector3(0.0f, 0.0f, attackAngle)) * transform.right;
            Debug.DrawRay(transform.position, throwRay * 20.0f, Color.red, 30.0f);

            // Calculate the force to throw the obj.
            throwRay.x *= Random.Range(_enemyDamageData.ThrowForce.x - _enemyDamageData.ThrowForceRange.x,
                                        _enemyDamageData.ThrowForce.x + _enemyDamageData.ThrowForceRange.x);
            throwRay.y *= Random.Range(_enemyDamageData.ThrowForce.y - _enemyDamageData.ThrowForceRange.y,
                                        _enemyDamageData.ThrowForce.y + _enemyDamageData.ThrowForceRange.y);

            GameObject throwObj = Instantiate(_enemyDamageData.ThrowObj, transform.position, Quaternion.identity);
            Rigidbody throwRB = throwObj.GetComponent<Rigidbody>();
            Debug.Assert(throwRB, "ThrowAttack: Update: ThrowObj's rigidbody not assigned!");
            throwRB.AddForce(throwRay, ForceMode.Impulse);
        }
    }
}
