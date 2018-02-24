using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {
	//unity inspector fields
	public GameObject launchPoint;
	public GameObject prefabProjectile;
	public float velocityMult = 4f;
	public bool _________________________;
	//fields set dynamically
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;
	public Rigidbody rb;

	// Use this for initialization
	void Awake() 
	{
		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
		rb = prefabProjectile.GetComponent <Rigidbody> ();
	}

	void OnMouseEnter () 
	{
		print ("Slingshot:OnMouseEnter()");
		launchPoint.SetActive (true);
	}
	
	// Update is called once per frame
	void OnMouseExit () 
	{
		print ("Slingshot:OnMouseExit()");
		launchPoint.SetActive (false);
	}

	void OnMouseDown()
	{
	//players pressed the mouse button while over slingshot to aim
		aimingMode = true;
		//instantiate projectile
		projectile = Instantiate (prefabProjectile)as GameObject;
		//start at launchPoint
		projectile.transform.position = launchPos;
		//set it to isKinematic for now
	}

	void Update ()
	{
		//if slingshot is not in aimingMode, ignore the code
		if (!aimingMode) return;
		//get current mouse position in 2d coordinates
		Vector3 mousePos2D = Input.mousePosition;
		//convert 2D mouse pos -> 3D mouse pos
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
		//finding delta from launchPos to mousePos3D
		Vector3 mouseDelta = mousePos3D -launchPos;
		//limit mouseDelta to the radius of Slingshot's SphereCollider
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;
		if (mouseDelta.magnitude > maxMagnitude)
		{
			mouseDelta.Normalize ();
			mouseDelta *= maxMagnitude;
		}
		//moves projectile to new position
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp (0)) 
		{
			//the mouse has been released
			aimingMode = false;
			rb.isKinematic = false;
			rb.velocity = -mouseDelta * velocityMult;
			projectile = null;
		}
	}
}
