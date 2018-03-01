using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
	public float jumpPower;
	public Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		GetComponent<SphereCollider> ().isTrigger = false;
		GetComponent<Renderer> ().material.color = Color.blue;
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision cos) 
	{
		if(cos.gameObject.CompareTag ("Finish"))
		{
			GetComponent<Renderer> ().material.color = Color.green;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce (0,jumpPower,0);
			GetComponent<Renderer> ().material.color = Color.red;
		}	
	}
}
