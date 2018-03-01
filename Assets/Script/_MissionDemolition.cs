using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
	idle,
	playing,
	levelEnd,
}

public class _MissionDemolition : MonoBehaviour {
	static public _MissionDemolition S; //singleton
	[Header("inspector Pane field")]
	public GameObject[] castles; //castle arrays
	public Text gtLevel;
	public Text gtScore;
	public Vector3 castlePos; //castle position
	[Header("Dynamic field")]
	public int level; //current level
	public int levelMax;// maximum levels
	public int shotsTaken;
	public GameObject castle;
	public GameMode mode = GameMode.idle;
	public string showing = "Slingshot"; //followcam mode

	// Use this for initialization
	void Start () 
	{
		S = this; // defining singleton
		level = 0;
		levelMax = castles.Length;
		StartLevel ();
	}

	void StartLevel ()
	{
		if (castle != null) 
		{
			Destroy (castle);
		}
		//destroy any available projectiles
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
		foreach (GameObject pTemp in gos) 
		{
			Destroy (pTemp);
		}
		//instantiate new castle
		castle = Instantiate(castles[level])as GameObject;
		castle.transform.position = castlePos;
		shotsTaken = 0;

		//reset camera
		SwitchView("Both");
		ProjectileLine.S.Clear ();
		//reset goal
		Goal.goalMet = false;
		ShowGT ();
		mode = GameMode.playing;
	}
	void ShowGT()
	{
		//show text data
		gtLevel.text = "Level:" + (level+1) + "of " + levelMax;
		gtScore.text = "Shots Taken:" + shotsTaken;
	}
	// Update is called once per frame
	void Update () 
	{
		ShowGT ();
		//check for level end
		if (mode==GameMode.playing && Goal.goalMet)
		{
			mode = GameMode.levelEnd;
			//zoom out
			SwitchView("Both");
			//start level in 2s
			Invoke("NextLevel", 2f);
		}
	}
	void OnGUI()
	{
		//draw gui button for view switching at top of screen
		Rect buttonRect = new Rect((Screen.width/2)-50,10,100,24);
		switch (showing) 
		{
		case "Slingshot":
			if (GUI.Button (buttonRect, "Show Castle")) {
				SwitchView ("Castle");
			}
			break;
		case "Castle":
			if (GUI.Button (buttonRect, "Show Both")) {
				SwitchView ("Both");
			}
			break;
		case "Both":
			if (GUI.Button (buttonRect, "Show Slingshot")) {
				SwitchView ("Slingshot");
			}
			break;
		}
	}

	static public void SwitchView(string eView)
	{
		S.showing = eView;
		switch (S.showing) 
		{
		case "Slingshot":
			FollowCam.S.poi = null;
			break;
		case "Castle":
			FollowCam.S.poi = S.castle;
			break;
		case "Both":
			FollowCam.S.poi = GameObject.Find("ViewBoth");
			break;
		}
	}
	//static method that allows code anywhere to increase shotTaken
	public static void ShotsFired()
	{
		S.shotsTaken++;
	}
}
