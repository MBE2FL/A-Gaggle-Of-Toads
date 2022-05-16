using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public abstract class Mechanic : MonoBehaviour
{
	bool _isPickedUp = false;
	Collider _collider;
	//	Rigidbody _rigidbody;
	MeshRenderer _meshRenderer;
	GameObject _target;
	MainInput _mainInput;
	MainInput.InGameActions _inGameActions;
	[SerializeField]
	float _timeToEnablePickup = 1.0f;
	public ButtonType buttonType { get; protected set; } = ButtonType.NONE;

	public enum ButtonType
	{
		NONE,
		BUTTON_1, // east
		BUTTON_2, // west
		BUTTON_3, // south
	}

	public bool IsPickedUp { get => _isPickedUp; }

	public GameObject Target { get => _target; }

	public MainInput.InGameActions InGameActions { get => _inGameActions; }



	private void Awake()
	{
		Init();
	}

	private void Update()
	{
		if(_isPickedUp)
			transform.localPosition = Vector3.zero;
	}

	protected virtual void Init()
	{
		_collider = GetComponent<Collider>();
		Debug.Assert(_collider, "Mechanic: Awake: Collider not assigned!");
		_collider.isTrigger = true;

		_meshRenderer = GetComponent<MeshRenderer>();
		Debug.Assert(_meshRenderer, "Mechanic: Awake: MeshRenderer not assigned!");


		_mainInput = new MainInput();
		Debug.Assert(_mainInput != null, "Jump: Awake: MainInput not assigned!");
		_inGameActions = _mainInput.InGame;
		//_inGameActions.Disable();
	}

	public abstract void Use();

	public virtual void OnPickedUp(GameObject target)
	{
		_isPickedUp = true;
		_collider.enabled = false;
		//_rigidbody.isKinematic = false;
		_meshRenderer.enabled = false;

		_target = target;
		if(Target.transform.tag == "Player")
		{
			_inGameActions.Enable();
		}
	}

	public virtual void OnDropped()
	{
		_isPickedUp = false;
		//_collider.enabled = true;
		_meshRenderer.enabled = true;

		if(Target.transform.tag == "Player")
		{
			_inGameActions.Disable();
		}
		_target = null;

		IEnumerator WaitToEnablePickup()
		{
			yield return new WaitForSeconds(_timeToEnablePickup);
			_collider.enabled = true;
			Debug.Log("Mechanic available for pickup!");
		}

		StartCoroutine(WaitToEnablePickup());
	}

}
