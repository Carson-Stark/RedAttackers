using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Animator anim;
	public GameObject[] buttons;

	public void Clicked () { //when play is clicked play click sound
		DataManager.PlayClick();
	}

	public void AnDone () { //when the animation is done destroy title and button
		Destroy (transform.GetChild (0).gameObject);
		Destroy (transform.GetChild (2).gameObject);

		anim.SetTrigger ("anDone");
	}

	public void An2Done () { //stop animation when done
		anim.enabled = false;
	}

	public void SingleClicked () { //when singleplayer is clicked destroy sing and multi buttons and set resume and newgame active
		foreach (GameObject button in buttons) {
			if (button.name == "Resume" || button.name == "NewGame"){
				button.SetActive (true);
			}
			else
				Destroy(button);
		}

		anim.enabled = true;
		anim.SetTrigger ("an2Done");

		DataManager.PlayClick();
	}

	public void Resume () { //when resume is clicked prepare level and load previous level
		if (PlayerPrefs.GetString("Level", "null") != "null"){
			DataManager.level = PlayerPrefs.GetString("Level");
			DataManager.diff = PlayerPrefs.GetString("Diff");
			DataManager.resuming = true;

			Application.LoadLevel (DataManager.level);
		}
		else { //if no previous game found make a new game
			NewGame ();

			Debug.Log ("no previous game found; starting new game");
		}
	}

	public void NewGame () { //when new game is clicked load next menu
		DataManager.level = "";
		DataManager.diff = "";
		DataManager.resuming = false;

		Application.LoadLevel (1);
	}
}
