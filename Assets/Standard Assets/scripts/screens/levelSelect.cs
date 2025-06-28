using UnityEngine;
using System.Collections;

public class levelSelect : MonoBehaviour {

	void Awake (){
		DataManager.PlayClick(); //play click audio
	}
	
	public void Clicked () {
		Application.LoadLevel (2); //load next menu
	}

	public void pillars () {
		DataManager.level = "pillars";

		Clicked ();
	}
}
