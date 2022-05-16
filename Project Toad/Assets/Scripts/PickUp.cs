using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static MainInput;

public class PickUp : MonoBehaviour
{

	public UnityEvent onPickupObject = new UnityEvent();
	public UnityEvent onPutDownObject = new UnityEvent();
	MainInput controls; MainInput.InGameActions play;
	List<Mechanic> m_mecanics = new List<Mechanic>();

	private void OnTriggerStay(Collider other)
	{
		//Mechanic Logic
		if(other.gameObject.TryGetComponent<Mechanic>(out var mechanic))
			if(pickupPressed)
			{
				mechanic.transform.SetParent(transform);
				mechanic.OnPickedUp(gameObject);
				onPickupObject.Invoke();
				m_mecanics.Add(mechanic);
			}
	}

	public bool pickupPressed { get; private set; } = false;

	int lastChildCount = 0;
	Mechanic.ButtonType dropType = Mechanic.ButtonType.NONE;
	public void OnPickUp(InputAction.CallbackContext ctx)
	{
		pickupPressed = ctx.performed;

		if(ctx.started)
			lastChildCount = transform.childCount;


		if(ctx.canceled)
		{
			var mechs = transform.GetComponentsInChildren<Mechanic>();

			foreach(var mech in mechs)
			{
				if(mech.buttonType == dropType)
				{

					IEnumerator CoDrop()
					{
						for(int a = 0; a < 5; ++a)
							yield return null;
						mech.transform.SetParent(null);
						mech.OnDropped();
						m_mecanics.Remove(mech);
						dropType = Mechanic.ButtonType.NONE;
					}

					StartCoroutine(CoDrop());
				}
			}

		}

	}

	void OnPutDown(InputAction.CallbackContext ctx)
	{
		if(ctx.performed)

		{
			if(ctx.control.path == ctx.action.controls[1].path)
				dropType = Mechanic.ButtonType.BUTTON_1;
			if(ctx.control.path == ctx.action.controls[2].path)
				dropType = Mechanic.ButtonType.BUTTON_2;
			if(ctx.control.path == ctx.action.controls[3].path)
				dropType = Mechanic.ButtonType.BUTTON_3;
		}


	}

	private void OnEnable()
	{
		if(controls == null)
		{
			controls = new MainInput();
			play = controls.InGame;
			play.PickUp.started += t => OnPickUp(t);
			play.PickUp.performed += t => OnPickUp(t);
			play.PickUp.canceled += t => OnPickUp(t);

			play.PutDown.started += t => OnPutDown(t);
			play.PutDown.performed += t => OnPutDown(t);
			play.PutDown.canceled += t => OnPutDown(t);
		}
		play.Enable();
	}

	void OnDisable() =>
		play.Disable();
}
