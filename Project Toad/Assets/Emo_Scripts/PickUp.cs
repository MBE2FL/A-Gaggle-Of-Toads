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
		//Item Logic
		if(other.gameObject.TryGetComponent<Item>(out var item))
			if(pickupPressed)
			{
				item.transform.SetParent(transform);
				item.gameObject.SetActive(false);
				PickupObject.Invoke();

			}
	}

	public bool pickupPressed { get; private set; } = false;

	int lastChildCount = 0;
	public void OnPickUpDown(InputAction.CallbackContext ctx)
	{
		pickupPressed = ctx.performed;
		
		if(ctx.started)
			lastChildCount = transform.childCount;
		
		if(ctx.canceled)
			if(transform.childCount == lastChildCount)
			{
				if(transform.childCount > 1)
				{
					var obj = transform.GetChild(1).gameObject;
					obj.SetActive(true); obj.transform.SetParent(null);
				}
			}
	}

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
