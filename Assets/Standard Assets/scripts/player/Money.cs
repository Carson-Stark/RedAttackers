using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {

	public bool infMoney;
	public int startingMoney;
	public GUIStyle style;

	public int currentMoney; //used by other scripts

	public int DefenceValue; //used by other scripts

	int tutShowed;

	void Awake () {
		if (!infMoney){
			if (DataManager.resuming) {
				tutShowed = 1;
				currentMoney = PlayerPrefs.GetInt ("Money", startingMoney);
			}
			else{
				tutShowed = 0;
				currentMoney = startingMoney;
			}
		}
	}

	void OnGUI () {
		if (currentMoney == 150 && tutShowed < 1 && !infMoney){

			GetComponent<tutorial>().enabled = true;
			Camera.main.GetComponent<tutorial>().show("$150tut");
			tutShowed++;
		}

		style.fontSize = Screen.height / 8;

		if (!infMoney) //display money if infM is not turned on, if it is turned on display inf
			GUI.Label(new Rect (10, Screen.height - Screen.height / 7, Screen.width / 2, Screen.height / 6), "Money: $" + currentMoney, style);
		else
			GUI.Label(new Rect (10, Screen.height - Screen.height / 7, Screen.width / 2, Screen.height / 6), "Money: Inf", style);
	}
}
