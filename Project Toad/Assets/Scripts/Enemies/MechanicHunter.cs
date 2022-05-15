using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MechanicHunter : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    PickUp _currentTarget;
    List<PickUp> _allPickUpsInRange;
    public UnityEvent<PickUp> _onPickedUp;



    public PickUp CurrentTarget
    {
        get
        {
            return _currentTarget;
        }
    }

    public void dropPickUp()
    {
        _currentTarget = null;
        searchForNearestPickUp();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void searchForNearestPickUp()
    {
        PickUp nearestPickUp = null;
        foreach (PickUp pickUp in _allPickUpsInRange)
        {
            if (Vector3.SqrMagnitude(transform.position - pickUp.transform.position) < (_navMeshAgent.remainingDistance * _navMeshAgent.remainingDistance))
            {
                nearestPickUp = pickUp;
            }
        }

        if (nearestPickUp)
        {
            _currentTarget = nearestPickUp;
            _navMeshAgent.SetDestination(_currentTarget.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PickUp>(out var pickUp))
        {
            if (_currentTarget)
            {
                if (_currentTarget != pickUp &&
                    (Vector3.SqrMagnitude(transform.position - pickUp.transform.position) < (_navMeshAgent.remainingDistance * _navMeshAgent.remainingDistance)))
                {
                    _currentTarget = pickUp;
                    _navMeshAgent.SetDestination(_currentTarget.transform.position);
                }
            }
            else
            {
                _currentTarget = pickUp;
                _navMeshAgent.SetDestination(_currentTarget.transform.position);
            }


            if (_currentTarget != pickUp)
            {
                _allPickUpsInRange.Add(pickUp);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PickUp>(out var pickUp))
        {
            if (_currentTarget != pickUp)
            {
                _allPickUpsInRange.Remove(pickUp);
            }
        }
    }
}
