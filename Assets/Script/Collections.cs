using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collections : MonoBehaviour {
	public List <GameObject> personList;
	public GameObject[] waypoints;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) 
		{
			//personList [0].GetComponent<People> ().personName;
		}
	}
}
