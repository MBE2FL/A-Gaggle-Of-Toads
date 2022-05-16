using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croutch : Mechanic
{
	[SerializeField, Range(0.001f, float.PositiveInfinity)] float crouchScale = .5f;
	
	protected override void Init()
	{
		base.Init();
		InGameActions.Crouch.performed += t => Use();

		buttonType = ButtonType.BUTTON_1;
	}

	bool crouchToggle = false;
	public override void Use()
	{
		Target.transform.localScale *= !crouchToggle ? crouchScale : 1 / crouchScale; crouchToggle = !crouchToggle;
	}

	public override void OnPickedUp(GameObject target)
	{
		base.OnPickedUp(target.GetComponentInChildren<Collider>().gameObject);
	}

	public override void OnDropped()
	{
		Use();
		base.OnDropped();
	}

}
