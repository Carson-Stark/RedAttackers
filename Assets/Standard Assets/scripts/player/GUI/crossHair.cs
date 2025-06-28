using UnityEngine;
using System.Collections;

public class crossHair : MonoBehaviour {

	public Texture2D crosshair;
	public bool showCrossHair;
	public bool trans;

	void Awake (){
		trans = true;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	void OnGUI () {
		float width = Screen.width / 2 - Screen.height / 20; //sets crosshair position
		float height = Screen.height / 2 - Screen.height / 20;

		if (trans) //sets crosshair trans.
			GUI.color = new Color (1f, 1f, 1f, 0.3f);
		else
			GUI.color = new Color (1f, 1f, 1f, 1f);

		if (showCrossHair) //shows crosshair
			GUI.Label(new Rect (width, height, Screen.height / 10, Screen.height / 10), crosshair);
	}
}
