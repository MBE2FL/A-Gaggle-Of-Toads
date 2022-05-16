using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MechanicHunter : MonoBehaviour
{
	NavMeshAgent _navMeshAgent;
	Mechanic _currentTarget;
	List<Mechanic> _allMechanicsInRange;
	public UnityEvent<Mechanic> _onPickedUp;



	public Mechanic CurrentTarget { get => _currentTarget; }

	public void dropMechanic()
	{
		_currentTarget = null;
		searchForNearestPickUp();
	}

	void searchForNearestPickUp()
	{
		Mechanic nearestMechanic = null;
		foreach(Mechanic pickUp in _allMechanicsInRange)
		{
			if(Vector3.SqrMagnitude(transform.position - pickUp.transform.position) < (_navMeshAgent.remainingDistance * _navMeshAgent.remainingDistance))
			{
				nearestMechanic = pickUp;
			}
		}

		if(nearestMechanic)
		{
			_currentTarget = nearestMechanic;
			_navMeshAgent.SetDestination(_currentTarget.transform.position);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent<Mechanic>(out var pickUp))
			PickUpMechanic(pickUp);	
	}

	public void PickUpMechanic(Mechanic mechanic)
	{
		if(_currentTarget)
		{
			if(_currentTarget != mechanic &&
				(Vector3.SqrMagnitude(transform.position - mechanic.transform.position) < (_navMeshAgent.remainingDistance * _navMeshAgent.remainingDistance)))
			{
				_currentTarget = mechanic;
				_navMeshAgent.SetDestination(_currentTarget.transform.position);
			}
		}
		else
		{
			_currentTarget = mechanic;
			_navMeshAgent.SetDestination(_currentTarget.transform.position);
		}


		if(_currentTarget != mechanic)
		{
			_allMechanicsInRange.Add(mechanic);
		}
		_onPickedUp.Invoke(mechanic);
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.TryGetComponent<Mechanic>(out var pickUp))
		{
			if(_currentTarget != pickUp)
			{
				_allMechanicsInRange.Remove(pickUp);
			}
		}
	}
}
