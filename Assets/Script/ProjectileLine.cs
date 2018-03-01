using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour {
	static public ProjectileLine S;//Singleton
	[Header ("Inspector Pane Field")]
	public float minDist = 0.1f;
	[Header("Dynamic field")]
	public LineRenderer line;
	GameObject _poi;
	public List<Vector3> points;
	// Use this for initialization
	void Awake () 
	{
		S = this; //setting singleton
		//finding reference for LineRenderer
		line = GetComponent<LineRenderer>();
		//disable linerenderer until needed
		line.enabled = false;
		//initialise the points list
		points = new List<Vector3>();
	}

	public GameObject poi {
		get {
			return(_poi);
		}

		set {
			_poi = value;
			if (_poi != null) {
				line.enabled = false;
				points = new List<Vector3> ();
				AddPoint ();
			}
		}
	}
	public void AddPoint()
	{
		//adds point in the line
		Vector3 pt = _poi.transform.position;
		if (points.Count > 0 && (pt- lastPoint))
		{
			//if the point isnt far enough from last point, return
			return;
		}

		if (points.Count == 0)
		{
			Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
			Vector3 launchPosDif = pt - launchPos;
			//if this is launch point, it adds extra bit of line to help aiming
			points.Add(pt+launchPosDif);
			points.Add (pt);
			line.SetVertexCount (2);
			//sets first 2 points
			line.SetPosition (0, points[0]);
			line.SetPosition (1, points [1]);
			line.enabled = true;
			//enables linerenderer
		}
		else
		{
			//normal behavior of adding a point
			points.Add(pt);
			line.SetVertexCount (points.Count);
			line.SetPosition (points.Count - 1, lastPoint);
			line.enabled = true;
		}
	}

	public Vector3 lastPoint
	{
		get {
			if (points==null)
			{
				//if there are no points, returns vector3.zero
				return (Vector3.zero);
			}
		}
	}

	void FixedUpdate()
	{
		if (poi == null)
		{
			//if there is no poi, search for one
			if (FollowCam.S.poi != null)
			{
				poi = FollowCam.S.poi;
			}
			else
			{
				return;
				//if no poi found, then re
			}
		}
		else
		{
			return;
		}
	}
}
