using UnityEngine;
using System.Collections;

public class tutorial : MonoBehaviour {

	public GameObject TutScreen;
	public GameObject[] Tuts;

	public GameObject MoneyTut;
	public GameObject MultiplierTut;

	public bool showTut;

	GameObject screen;
	int count;

	void Awake () {
		if (DataManager.diff == "Beginner" || DataManager.diff == "Easy"){ //enable tut if on low difficulty
			if (!DataManager.resuming)
				showTut = true;
		}

		if (showTut){
			stopGame();

			screen = (GameObject)Instantiate (TutScreen, TutScreen.transform.position, TutScreen.transform.rotation); //inst intro
		}
	}

	void Update () {
		if (showTut){
			if (Input.GetMouseButtonDown(0)){
				Destroy (screen); //destroy previous screen 

				if (count < Tuts.Length){
					screen = (GameObject)Instantiate (Tuts[count], Tuts[count].transform.position, Tuts[count].transform.rotation); //instantiate tut based on count
					count++;
				}
				else{
					startGame();
				}
			}
		}
	}

	public void show (string tut) {
		if (showTut){
			stopGame();

			if (tut == "$150tut") 
				screen = (GameObject)Instantiate (MoneyTut, MoneyTut.transform.position, MoneyTut.transform.rotation); //instantiate tut based on count
			else if (tut == "multiplierTut")
				screen = (GameObject)Instantiate (MultiplierTut, MultiplierTut.transform.position, MultiplierTut.transform.rotation); //instantiate tut based on count
			else
				Debug.LogError("string tut defaulted, tutorial.show()");
		}
	}

	void stopGame () {
		Time.timeScale = 0; //disable functions
		GetComponent<GUI_Buttons>().enable = false;
		GetComponent<crossHair>().showCrossHair = false;
		transform.parent.GetComponent<MouseLook>().enabled = false;
		transform.parent.GetComponentInChildren<Gun>().enabled = false;
	}
	
	void startGame () {
		Time.timeScale = 1; //enable functions
		GetComponent<GUI_Buttons>().enable = true;
		if (!GetComponent<GUI_Buttons>().topView) {
			GetComponent<crossHair>().showCrossHair = true;
			transform.parent.GetComponent<MouseLook>().enabled = true;
			transform.parent.GetComponentInChildren<Gun>().enabled = true;
		}

		enabled = false;
	}
}
