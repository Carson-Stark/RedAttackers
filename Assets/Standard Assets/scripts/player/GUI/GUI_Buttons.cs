using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GUI_Buttons : MonoBehaviour {
	
	public Transform center;

	public float camAnimationSpeed;
	public float savingDisplayTime;

	public GUISkin Bskin;
	public GUISkin Uskin;
	public GUISkin Xskin;
	public GUISkin Oskin;
	public GUISkin Pskin;
	public GUISkin FFskin;
	public GUISkin ErrorSkin;
	public GUIStyle style;

	public GameObject buildScreen;
	public GameObject pauseScreen;

	public bool screenOn; //used by other scripts
	public bool building; //used by other scripts
	public bool Enabled; //used by other scripts

	public bool removing; //used by other scripts

	public bool upgrading; //used by other scripts

	public bool paused; //used by other scripts

	public bool saving; //used by other scripts

	public bool topView; //used by other scripts

	public bool enable; //used by other scripts

	public bool onPath; //used by other scripts

	public int ffSpeed; //used by other scripts

	GameObject player; //refrence to player
	Vector3 lastCamPos;
	Quaternion lastCamRo;
	string key;
	bool movingCam;
	bool ffing;
	float savingTime;
	int enemyLeft;

	void Awake () {
		AudioListener.volume = 1; //set sound default to 1;
		DataManager.PlayClick();

		player = GameObject.FindGameObjectWithTag ("Player");

		ffSpeed = PlayerPrefs.GetInt ("lastFFSpeed", 0);

		switch (ffSpeed) {
			case 0:
				ffSpeed = 2;
				break;
			case 1:
				ffSpeed = 5;
				break;
			case 2:
				ffSpeed = 10;
				break;
			case 3:
				ffSpeed = 30;
				break;
			case 4:
				ffSpeed = 60;
				break;
		}

		screenOn = false;

		Enabled = true;

		movingCam = false;
		topView = false;

		paused = false;
		ffing = false;

		onPath = false;

		savingTime = savingDisplayTime;
	}

	void OnGUI () {
		enemyLeft = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemy>().enemyStanding;

		if (enable)
			GUI.enabled = true;
		else{
			GUI.color = new Color(1,1,1,2);
			GUI.enabled = false;
		}

		if (saving)
			savingTime -= Time.deltaTime;
		if (savingTime <= 0){
			savingTime = savingDisplayTime;
			saving = false;
		}

		style.fontSize = Screen.height / 20;//set size of font based on screen

		if (!screenOn && !building && !removing && !upgrading && !paused){

			if (!topView && !ffing) {

				GUI.skin = Bskin;
				Bskin.button.fontSize = Screen.height / 10; //set size of font based on screen size

				if (Enabled && (GUI.Button(new Rect (Screen.width - Screen.width / 4, Screen.height - Screen.height / 7, Screen.width / 4, Screen.height / 7), "Build") || key == "b") && enable){ //display build button
					Instantiate(buildScreen, buildScreen.transform.position, buildScreen.transform.rotation); //display screen
				}

				GUI.skin = Uskin;
				Uskin.button.fontSize = Screen.height / 13; //set size of font based on screen size

				if ((GUI.Button(new Rect (Screen.width - Screen.width / 4, Screen.height - Screen.height / 3.5f, Screen.width / 4, Screen.height / 7), "Upgrade") || key == "u") && enable){ //display up button
					upgrading = true;

					GetComponent<upgradeDefence>().enabled = true;
				}

				GUI.skin = Xskin;  

				if ((GUI.Button(new Rect (Screen.width - Screen.width / 4.1f, Screen.height - Screen.height / 1.017f, Screen.width / 15, Screen.height / 10), "") || key == "x") && enable){ //display remove button
					removing = true;

					GetComponent<removeDefence>().enabled = true;
				}

			}

			GUI.skin = Oskin;

			if (onPath)
				GUI.skin = ErrorSkin;
		
			if (!ffing && !movingCam && (GUI.Button(new Rect (Screen.width - Screen.width / 6.3f, Screen.height - Screen.height / 1.017f, Screen.width / 15, Screen.height / 10), "") || key == "t") && !onPath && enable){ //display top veiw button

				if (topView) {
					player.transform.Find ("PlaceHolder!!!!").GetComponent<Gun>().enabled = true;
					player.GetComponent<MouseLook>().enabled = true;
					player.GetComponent<MoveCharacter>().enabled = true;
				
					GetComponent<crossHair>().showCrossHair = true;

					transform.position = lastCamPos;
					transform.rotation = lastCamRo;

					topView = false; 
	
				}
				else {
					player.transform.Find ("PlaceHolder!!!!").GetComponent<Gun>().enabled = false;
					player.GetComponent<MouseLook>().enabled = false;
					player.GetComponent<MoveCharacter>().enabled = false;
						
					GetComponent<crossHair>().showCrossHair = false;

					lastCamPos = transform.position;
					lastCamRo = transform.rotation;

					movingCam = true;
					topView = true;
				}
			}

			GUI.skin = FFskin;

			if (onPath)
				GUI.skin = ErrorSkin;

			if (enemyLeft == 0){
				ffing = false;
				Time.timeScale = 1;
				AudioListener.volume = 1;
			}
			if (enemyLeft != 0 && (GUI.Button(new Rect (Screen.width / 4.2f, Screen.height - Screen.height / 1.017f, Screen.width / 15, Screen.height / 10), "") || key == "f") && !onPath && enable){ //display skip button
				if (ffing) {
					ffing = false;
					Time.timeScale = 1;
					AudioListener.volume = 1;
				}
				else {
					ffing = true;

					if (!topView) {
						player.transform.Find ("PlaceHolder!!!!").GetComponent<Gun>().enabled = false;
						player.GetComponent<MouseLook>().enabled = false;
						player.GetComponent<MoveCharacter>().enabled = false;
				
						GetComponent<crossHair>().showCrossHair = false;
				
						lastCamPos = transform.position;
						lastCamRo = transform.rotation;
				
						movingCam = true;
						topView = true;
					}
					
					AudioListener.volume = 0;
					Time.timeScale = ffSpeed;	
				}
			}
		}

		GUI.skin = Pskin;

		if (!paused && (GUI.Button(new Rect (Screen.width - Screen.width / 13.5f, Screen.height - Screen.height / 1.017f, Screen.width / 15, Screen.height / 10), "") || key == "p") && enable) { //display pause button
			Instantiate (pauseScreen, pauseScreen.transform.position, pauseScreen.transform.rotation);
			GetComponent<stopGame>().StopGame();
		}

		if (!screenOn){
			if (building) //display how-to text when building
				GUI.Label (new Rect(Screen.width / 2 - 5000, Screen.height / 4 - 50, 10000, 200), "Right click to place defence; C = Cancel", style);
			else if (removing) //display how-to text when removing
				GUI.Label (new Rect(Screen.width / 2 - 5000, Screen.height / 4 - 50, 10000, 200), "Right click to remove selected defence; C = Cancel", style);
			else if (upgrading) { // display how-to text when upgrading
				GUI.Label (new Rect(Screen.width / 2 - 500, Screen.height / 4 - 50, 1000, 5000), "Right click to select defence; Press G to upgrade gun; C = Cancel", style);
				//GUI.Label (new Rect(Screen.width - Screen.width / 3, Screen.height - Screen.height / 2, 400, 200), "Press G to upgrade gun", style);
			}
			else if (saving) // display saving text before wave begins
				GUI.Label (new Rect(Screen.width / 2 - 5000, Screen.height / 20, 1000, 200), "Saving...", style);
			else if (onPath) // display warning when on path
				GUI.Label (new Rect(Screen.width / 2 - 5000, Screen.height / 4 - 50, 10000, 200), "You may not fast forward when you are on the path", style);
		}

		key = null; //reset input
	}

	void Update () {
		if (movingCam) {
			transform.LookAt (center);
			Vector3 targetPosition = new Vector3 (center.position.x, center.position.y + 60, center.position.z);

			transform.position = Vector3.MoveTowards (transform.position, targetPosition, camAnimationSpeed);

			if (transform.position == targetPosition) {
				movingCam = false;
				transform.rotation = center.rotation;
			}
		}

		foreach(KeyCode kcode in System.Enum.GetValues (typeof (KeyCode))) {
			if (Input.GetKeyDown(kcode))
				key = kcode.ToString().ToLower();
		}
	}
}