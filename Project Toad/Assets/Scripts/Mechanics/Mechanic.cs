using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public abstract class Mechanic : MonoBehaviour
{
	bool _isPickedUp = false;
	Collider _collider;
	MeshRenderer _meshRenderer;
	GameObject _target;
	MainInput _mainInput;
	MainInput.InGameActions _inGameActions;



	public bool IsPickedUp
    {
        get
        {
			return _isPickedUp;
        }
    }

	public GameObject Target
    {
        get
        {
			return _target;
        }
    }

	public MainInput.InGameActions InGameActions
    {
        get
        {
			return _inGameActions;
        }
    }

	private void Awake()
	{
		init();
	}

	protected virtual void init()
    {
		_collider = GetComponent<Collider>();
		Debug.Assert(_collider, "Mechanic: Awake: Collider not assigned!");
		//coll.convex = true;
		_collider.isTrigger = true;
		_meshRenderer = GetComponent<MeshRenderer>();
		Debug.Assert(_meshRenderer, "Mechanic: Awake: MeshRenderer not assigned!");

		_mainInput = new MainInput();
		Debug.Assert(_mainInput != null, "Jump: Awake: MainInput not assigned!");
		_inGameActions = _mainInput.InGame;
		//_inGameActions.Disable();
	}

	public abstract void use();

	public virtual void onPickedUp(GameObject target)
    {
		_isPickedUp = true;
		_collider.enabled = false;
		_meshRenderer.enabled = false;

		_target = target;
		if (Target.transform.tag == "Player")
		{
			_inGameActions.Enable();
		}
	}

	public virtual void OnDropped()
    {
		_isPickedUp = false;
		//_collider.enabled = true;
		_meshRenderer.enabled = true;

		if (Target.transform.tag == "Player")
		{
			_inGameActions.Disable();
		}
		_target = null;

		StartCoroutine(WaitToEnablePickup());
	}

	IEnumerator WaitToEnablePickup()
    {
		yield return new WaitForSeconds(1.0f);
		_collider.enabled = true;
		Debug.Log("Mechanic available for pickup!");
	}
}
