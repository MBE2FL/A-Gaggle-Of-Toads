using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using static MainInput;
using static Point;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PickUp))]
public class PlayerMovement : MonoBehaviour, IInGameActions
{
	public Leaderboard leaderboard;
	public ShowLeaderboard showLeaderboard;
	public Timer timer;
	public SOMovementPerams perams;
	public UnityEvent EndReached = new UnityEvent();

	MainInput controls; MainInput.InGameActions play;

	/// <summary>
	/// makes sure the player can not move
	/// </summary>
	public bool stunned { get; set; } = false;
	Transform respawnPoint;

	bool move, rotate;

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name.ToLower() == "floor")
			respawn();
	}

	private void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.TryGetComponent<Point>(out var point))
			switch(point.type)
			{
			case PointType.START:
				//nothing I guess?
				timer?.begin();
				break;
			case PointType.CHECK:
				//respawn
				respawnPoint = point.transform;
				break;
			case PointType.END:

				timer?.end();
				EndReached.Invoke();
				showLeaderboard?.Show();
				break;
			}
	}


	void respawn()
	{
		transform.position = respawnPoint.position;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.GetChild(0).rotation = respawnPoint.rotation;
	}


	// Update is called once per frame
	void Update()
	{
		if(move && !stunned)
		{
			Vector3 forward = transform.forward * new float3(1, 0, 1);
			Vector3 right = transform.right * new float3(1, 0, 1);
			forward.Normalize();
			right.Normalize();

			//Force Movement
			GetComponent<Rigidbody>().
			AddForce(Vector3.Normalize(forward * pos.z + right * pos.x) * perams.moveForce, ForceMode.Force);
			//   GetComponent<Rigidbody>().;

			//velocity cap
			GetComponent<Rigidbody>().velocity =
			math.length(GetComponent<Rigidbody>().velocity * new float3(1, 0, 1)) > perams.moveMaxVel ?
			(Vector3)(GetComponent<Rigidbody>().velocity.normalized * new float3(perams.moveMaxVel, 0, perams.moveMaxVel) +
			new float3(0, GetComponent<Rigidbody>().velocity.y, 0)) :
			GetComponent<Rigidbody>().velocity;

			////Positional movement
			//      transform.position += Vector3.Normalize(forward * pos.z + right * pos.x) * Time.deltaTime * moveSpd;

		}
		else
		{
			//slowdown

			GetComponent<Rigidbody>().velocity -= (Vector3)(GetComponent<Rigidbody>().velocity * new float3(1, 0, 1)) * perams.slowPercent;

			if(math.length(GetComponent<Rigidbody>().velocity * new float3(1, 0, 1)) < .01f)
				GetComponent<Rigidbody>().velocity =
				new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

		}

		if(rotate)
		{
			rot += rotVec * Time.deltaTime * perams.rotSpd;
			transform.GetChild(0).rotation = Quaternion.Euler(rot);
			transform.rotation = Quaternion.Euler(rot * new float3(0, 1, 0));
		}
	}

	Vector3 pos;
	public void OnMovement(InputAction.CallbackContext ctx)
	{
		move = !ctx.canceled;

		if(!move) return;

		pos = new Vector3(DeadzoneValue(ctx.ReadValue<float>(), perams.deadzone), 0, 0).normalized;

	}

	float DeadzoneValue(float val, float deadZone) => ((Mathf.Abs(val) < deadZone) ? 0 : val);


	Vector3 rot, rotVec;
	public void OnRotation(InputAction.CallbackContext ctx)
	{
		//rotate = true;
		rotate = !ctx.canceled;
		if(!ctx.performed) return;

		//	rotVec = new Vector3(-ctx.ReadValue<Vector2>().y, ctx.ReadValue<Vector2>().x, 0);

		// Mouse.current.WarpCursorPosition(Vector2.zero);
		// print(ctx.ReadValue<Vector2>());

	}

	public void OnMouseRotation(InputAction.CallbackContext ctx) { OnRotation(ctx); rotVec *= 0.2f; }

	public void OnJump(InputAction.CallbackContext ctx)
	{
		if(!ctx.started) return;
		var body = GetComponent<Rigidbody>();
		var onfloor = Physics.Raycast(transform.position, Vector3.down, 1.07f);

		if(onfloor)
			body.AddForce(new Vector3(0, perams.jumpForce, 0), ForceMode.Impulse);
	}

	private void OnEnable()
	{
		var points = GameObject.FindObjectsOfType<Point>();

		foreach(var point in points)
			if(point.type == Point.PointType.START)
			{ respawnPoint = point.gameObject.transform; respawn(); break; }

		Physics.gravity = new Vector3(0, -33.141596f, 0);
		rot = transform.GetChild(0).rotation.eulerAngles;
		if(controls == null)
		{
			controls = new MainInput();
			play = controls.InGame;
			play.SetCallbacks(this);
		}
		play.Enable();
	}

	void OnDisable() =>
		play.Disable();

	public void OnPause(InputAction.CallbackContext context)
	{
		throw new System.NotImplementedException();
	}


	public void OnPickUpDown(InputAction.CallbackContext ctx)
	{

		
	}

	public void OnInventory(InputAction.CallbackContext context)
	{
		//	throw new System.NotImplementedException();
	}

	public void OnCrouch(InputAction.CallbackContext context)
	{
		throw new System.NotImplementedException();
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		throw new System.NotImplementedException();
	}

	public void OnYeet(InputAction.CallbackContext context)
	{
		throw new System.NotImplementedException();
	}
}
