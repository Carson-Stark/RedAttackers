using UnityEngine;
using System.Collections;

public class stopGame : MonoBehaviour {

	bool twoScreens = false;

	public void StopGame (){ //pause game
		Time.timeScale = 0;

		GetComponent<GUI_Buttons>().paused = true;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		GetComponent<GUI_Buttons>().building = false;
		if (GetComponent<GUI_Buttons>().screenOn)
			twoScreens = true;
		else 
			GetComponent<GUI_Buttons>().screenOn = true;

		GetComponent<crossHair>().showCrossHair = false;
		transform.parent.GetComponent<MouseLook>().enabled = false;
		transform.parent.GetComponentInChildren<Gun>().enabled = false;
	}

	public void PlayGame (){ //unpause game
		Time.timeScale = 1;

		GetComponent<GUI_Buttons>().paused = false;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		if (!twoScreens) {
			GetComponent<GUI_Buttons>().screenOn = false;
			if (!GetComponent<GUI_Buttons>().topView){
				transform.parent.GetComponent<MouseLook>().enabled = true;
				GetComponent<crossHair>().showCrossHair = true;
				transform.parent.GetComponentInChildren<Gun>().enabled = true;
			}
		}
		else
			twoScreens = false;
	}
}
