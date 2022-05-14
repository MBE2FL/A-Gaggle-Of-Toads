using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

	public UnityEvent<float> onTakeDamage = new UnityEvent<float>();
	public UnityEvent<float> onAddHealth = new UnityEvent<float>();
	public UnityEvent onNoHealth = new UnityEvent();

	public float val { get { return val; } private set { val = value; if(val <= 0) onNoHealth.Invoke(); } } 

	public void AddHealth(float inc) { val += inc; onTakeDamage.Invoke(val); }
	public void TakeDamage(float inc){ val += inc; onAddHealth.Invoke(val); }
}
