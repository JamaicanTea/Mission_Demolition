using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour {
	//fields for unity inspector pane
	public int numClouds = 40; //# of clouds to be made
	public GameObject[] cloudPrefabs; //cloud prefabs
	public Vector3 cloudPosMin; //min and max pos of clouds
	public Vector3 cloudPosMax; 
	public float cloudScaleMin = 1f; //min and max cloud scaling
	public float cloudScaleMax = 5f; 
	public float cloudSpeedMult = 0.5f;//cloud speed
	public bool ___________________________________;
	//dynamically filled fields
	public GameObject[] cloudInstances;

	void Awake ()
	{
		//make an array for cloundInstances
		cloudInstances = new GameObject [numClouds];
		//find cloudanchor parent gameobject
		GameObject anchor = GameObject.Find ("CloudAnchor");
		//Iterate thru and make cloud_s
		GameObject cloud;
		for (int i=0; i<numClouds; i++)
		{
			//pick an int between cloudPrefabs.length-1
			//Random.range will not pick as high as the top number
			int prefabNum = Random.Range (0,cloudPrefabs.Length);
			//create an instance
			cloud = Instantiate (cloudPrefabs [prefabNum])as GameObject;
			//position cloud
			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range (cloudPosMin.x, cloudPosMax.x);
			cPos.y = Random.Range (cloudPosMin.y, cloudPosMax.y);
			//scale cloud
			float scaleU = Random.value;
			float scaleVal = Mathf.Lerp (cloudScaleMin, cloudScaleMax, scaleU);
			//smaller clouds will be on the ground and farther away
			cPos.y= Mathf.Lerp (cloudPosMin.y, cPos.y, scaleU);
			cPos.z = 100 - 90 * scaleU;
			//apply these transformations to cloud
			cloud.transform.position = cPos;
			cloud.transform.localScale = Vector3.one * scaleVal;
			//make cloud child of anchor
			cloud.transform.parent = anchor.transform;
			//add cloud to cloudInstances
			cloudInstances[i] = cloud;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//iterate over each created clouds
		foreach (GameObject cloud in cloudInstances)
		{
			//get cloud's scale and position
			float scaleVal = cloud.transform.localScale.x;
			Vector3 cPos = cloud.transform.position;
			//moves larger cloud faster
			cPos.x -= scaleVal*Time.deltaTime *cloudSpeedMult;
			//if cloud moved too far to the left
			if (cPos.x <= cloudPosMin.x)
			{
				//move it to far right
				cPos.x = cloudPosMax.x;
			}
			//apply new position to cloud
			cloud.transform.position = cPos;
		}
	}
}
