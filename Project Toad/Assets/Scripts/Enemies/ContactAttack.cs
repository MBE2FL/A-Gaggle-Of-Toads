using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAttack : MonoBehaviour
{
    [SerializeField]
    EnemyDamageData _enemyDamageData;
    float _attackTimer;



    // Update is called once per frame
    void Update()
    {
        _attackTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_attackTimer >= _enemyDamageData.AttackSpeed)
        {
            if (collision.collider.tag == "Player")
            {
                Health health = collision.gameObject.GetComponent<Health>();
                Debug.Assert(health, "ContactAttack: OnCollisionEnter: Players's health not assigned!");
                health.TakeDamage(_enemyDamageData.Damage);
                _attackTimer = 0;

                Debug.Log("Attacked");
            }
        }
    }
}
