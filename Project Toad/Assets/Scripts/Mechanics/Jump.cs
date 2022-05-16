using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Jump : Mechanic
{
	//bool _isJumping;
	//bool _onGround;
	[SerializeField]
	float _jumpStrength = 5.0f;
	[SerializeField]
	float _groundCheckDistance = 0.1f;
	Collider _targetCollider;
	[SerializeField]
	int _jumpLayerMask = 1 << 7;
	Rigidbody _targetRigidbody;



	protected override void Init()
	{
		base.Init();
		InGameActions.Jump.performed += (context) => Use();
		buttonType = ButtonType.BUTTON_3;
	}

	// Update is called once per frame
	void Update()
	{
		if(IsPickedUp)
		{
			// Check if the character is on the ground or not.
			Vector3 origin = _targetCollider.transform.position;
			origin.y -= _targetCollider.bounds.extents.y;
			Ray ray = new Ray(origin, -_targetCollider.transform.up);
			Debug.DrawRay(origin, -_targetCollider.transform.up * _groundCheckDistance, Color.red);
		//	if(Physics.Raycast(ray, _groundCheckDistance, _jumpLayerMask))
		//	{
		//		_onGround = true;
		//	}
		//	else
		//	{
		//		_onGround = false;
		//	}
		//
		//	// Reset jump flag upon landing back on the ground.
		//	if(_isJumping && _onGround)
		//	{
		//		_isJumping = false;
		//	}
		//
		//
		//	if(Keyboard.current.rKey.isPressed)
		//	{
		//		OnDropped();
		//	}
		}
	}

	public override void Use()
	{
		//  if (!_isJumping && _onGround && IsPickedUp)
		//  {
		//      _isJumping = true;
		//
		//      Vector3 jumpForce = _jumpStrength * Target.transform.up;
		//      _targetRigidbody.AddForce(jumpForce, ForceMode.Impulse);
		//  }

		var min = _targetCollider.bounds.center;
		min.y = _targetCollider.bounds.min.y;
		var onfloor = Physics.Raycast(min + new Vector3(0, 0.01f, 0), Vector3.down, 0.5f);
		Debug.DrawRay(min + new Vector3(0, 0.08f, 0), Vector3.down, Color.red, 0.5f);

		Vector3 jumpForce = _jumpStrength * _targetCollider.transform.up;
		if(onfloor)
			_targetRigidbody.AddForce(jumpForce, ForceMode.Impulse);
	}

	public override void OnPickedUp(GameObject target)
	{
		base.OnPickedUp(target);
		
		_targetCollider = target.GetComponentInChildren<Collider>();
		Debug.Assert(_targetCollider, "Jump: onPickedUp: Collider not assigned!");
		_targetRigidbody = target.GetComponentInChildren<Rigidbody>();
		Debug.Assert(_targetRigidbody, "Rigid: onPickedUp: Rigidbody not assigned!");
	}

	public override void OnDropped()
	{
		base.OnDropped();
		
		_targetCollider = null;
		_targetRigidbody = null;
	}
}
