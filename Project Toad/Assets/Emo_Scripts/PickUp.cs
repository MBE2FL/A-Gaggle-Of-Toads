using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static MainInput;

public class PickUp : MonoBehaviour
{

	public UnityEvent PickupObject = new UnityEvent();
	MainInput controls; MainInput.InGameActions play;
	private void OnTriggerStay(Collider other)
	{

		if(other.gameObject.TryGetComponent<Item>(out var tmp2))
			if(pickupPressed)
			{
				tmp2.transform.SetParent(transform);
				(tmp2.gameObject).SetActive(false);
				PickupObject.Invoke();
			}

	}

	public bool pickupPressed { get; private set; } = false;

	public void OnPickUpDown(InputAction.CallbackContext ctx)=>	pickupPressed = ctx.performed;	

	private void OnEnable()
	{
		if(controls == null)
		{
			controls = new MainInput();
			play = controls.InGame;
			play.PickUpDown.started += t => OnPickUpDown(t);
			play.PickUpDown.performed += t => OnPickUpDown(t);
			play.PickUpDown.canceled += t => OnPickUpDown(t);
		}
		play.Enable();
	}

	void OnDisable() =>
		play.Disable();
}
