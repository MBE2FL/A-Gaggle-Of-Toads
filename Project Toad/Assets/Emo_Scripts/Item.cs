using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{


	private void Awake()
	{
		var coll=GetComponent<Collider>();
		//coll.convex = true;
		coll.isTrigger = true;
	}


}
