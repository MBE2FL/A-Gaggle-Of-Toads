using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbMechanic : MonoBehaviour
{
	Mechanic _currentMechanic;
	float _totalAbsorbTime;
	float _currAbsorbTime;
	bool _absorbed = false;
	MechanicHunter _mechanicHunter;



	void Awake()
	{
		_mechanicHunter = GetComponent<MechanicHunter>();
		Debug.Assert(_mechanicHunter, "AbsorbMechanic: Start: AbsorbMechanic's MechanicHunter not assigned!");
		_mechanicHunter._onPickedUp.AddListener(onPickedUp);
	}

	private void OnDestroy()
	{
		_mechanicHunter._onPickedUp.RemoveListener(onPickedUp);
	}

	// Update is called once per frame
	void Update()
	{
		if(_currentMechanic)
		{
			if(!_absorbed)
			{
				_absorbed = true;
				_currentMechanic.Use();
			}

			_currAbsorbTime += Time.deltaTime;

			if(_currAbsorbTime >= _totalAbsorbTime)
			{
				_currAbsorbTime = 0;
				_currentMechanic = null;
				_absorbed = false;
			}
		}
	}




	void onPickedUp(Mechanic mechanec)
	{
		_currentMechanic = mechanec;
		_currentMechanic.OnPickedUp(gameObject);
	}
}
