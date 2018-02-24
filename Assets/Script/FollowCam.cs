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
		cam = GetComponent <Camera> ();
	}

	void Update () 
	{
		//if there is only one line following an if, no braces needed
		if (poi == null) return; //no point of interest = return

		//get poi's position
		Vector3 destination = poi.transform.position;
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
		cam.orthographicSize = destination.y + 10;
			
	}
}
