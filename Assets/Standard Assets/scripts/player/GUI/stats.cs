using UnityEngine;
using System.Collections;

public class stats : MonoBehaviour {

	public bool showStats;

	public Texture2D background;
	public GUIStyle style;

	GameObject spawn;

	void Awake () {
		spawn = GameObject.FindGameObjectWithTag ("Spawn");
	}

	void OnGUI(){
		if (showStats){
			GUI.DrawTexture (new Rect (0, 0, Screen.width / 4.5F, Screen.height / 8), background); //draw background

			style.fontSize = Screen.height / 30; //set font size based on screen
				
			GUI.Label (new Rect(10, Screen.height / 90, 200, 200), "Wave: " + spawn.GetComponent<SpawnEnemy>().wave.ToString(), style); //display stats
			GUI.Label (new Rect(10, Screen.height / 25, 200, 200), "Diff: " + spawn.GetComponent<SpawnEnemy>().d, style);
			GUI.Label (new Rect(10, Screen.height / 15, 200, 200), "Multiplier: " + spawn.GetComponent<SpawnEnemy>().multiplier.ToString(), style);
		}
	}
}
