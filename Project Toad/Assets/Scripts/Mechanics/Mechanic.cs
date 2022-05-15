using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Mechanic : MonoBehaviour
{


	private void Awake()
	{
		var coll=GetComponent<Collider>();
		//coll.convex = true;
		coll.isTrigger = true;
	}



	public abstract void use();


}
