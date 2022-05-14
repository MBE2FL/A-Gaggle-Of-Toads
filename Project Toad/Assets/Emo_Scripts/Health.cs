using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

	public UnityEvent<byte> onTakeDamage = new UnityEvent<byte>();
	public UnityEvent<byte> onAddHealth = new UnityEvent <byte>();
	public UnityEvent onNoHealth = new UnityEvent();

	private void Awake()
	{
		val = 100;
	}

	public byte val { get { return val; } private set { val = value; if(val <= 0) onNoHealth.Invoke(); } }

	public void setHealth( byte val) => this.val = val;
	public void AddHealth( byte inc) { val += inc; onTakeDamage.Invoke(val); }
	public void TakeDamage(byte inc){ val += inc; onAddHealth.Invoke(val); }
}
