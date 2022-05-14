using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/MovementPerams")]
public class SOMovementPerams : ScriptableObject
{
	public float deadzone = .2f;
	public float moveForce = 50
		, jumpForce = 25
		, moveMaxVel = 15
		, rotSpd = 50;
	[Range(0, 1)]
	public float slowPercent = .1f;
}
