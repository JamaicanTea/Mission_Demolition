using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
	static public bool goalMet = false;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Projectile") 
		{
			goalMet = true;
			Color c = GetComponent<Renderer> ().material.color;
			c.a = 1;
			GetComponent<Renderer> ().material.color = c;
		}
	}

}
