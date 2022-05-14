using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

	public UnityEvent<byte> onTakeDamage = new UnityEvent<byte>();
	public UnityEvent<byte> onAddHealth = new UnityEvent<byte>();
	public UnityEvent onNoHealth = new UnityEvent();
	[SerializeField, Range(0, 255)] byte health = 0;
	bool dead = true;

	private void Awake()
	{
		val = 4;
		dead = false;
	}

	public byte val { get => health; private set { health = value; if(val <= 0 && !dead) { dead = true; onNoHealth.Invoke(); } } }

	public void setHealth(byte val) => this.val = val;
	public void AddHealth(byte inc) { val += inc; onAddHealth.Invoke(val); }
	public void TakeDamage(byte dec) { val -= dec; onTakeDamage.Invoke(val); }
}
