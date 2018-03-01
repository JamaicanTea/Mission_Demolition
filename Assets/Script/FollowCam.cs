using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	static public FollowCam S; //FollowCam Singleton

	// fields set in unity inspector pane
	public float easing = 0.05f;
	public Vector2 minXY;
	public Camera cam;
	public bool _________________________________;

	//fields set dynamically
	public GameObject poi; //Point of Interest
	public float camZ; //desired Z position of camera

	void Awake ()
	{
		S = this;
		camZ = this.transform.position.z;
	}
	void FixedUpdate ()
	{
		//if there is only one line following an if, no braces needed
		//no point of interest = return

		//get poi's position
		Vector3 destination;
		if (poi == null) {
			destination = Vector3.zero;
		}
		else 
		{
			//get poi's position
			destination = poi.transform.position;
			//if poi = projectile, check if it's at rest
			if(poi.tag == "Projectile")
			{
				//if it sleeps..
				if (poi.GetComponent<Rigidbody>().IsSleeping())
				{
					poi = null;
					return;
				}
			}
		}
		//limit x and y to minimum values
		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);
		//interpolate current camera position towards destination
		destination = Vector3.Lerp (transform.position, destination ,easing);
		//retain destination.z of camZ
		destination.z = camZ;
		//set camera to destination
		transform.position = destination;
		//setting ortographicSize of camera to keep ground in view
		this.cam.GetComponent <Camera> ().orthographicSize = destination.y + 10;
			
	}
			
}
