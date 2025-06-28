using UnityEngine;
using System.Collections;

public class WaveDisplay : MonoBehaviour {
	
	public string text = "Wave";
	
	int wave;

	void Awake () {
		wave = 1;
	}
	
	// Update is called once per frame
	void Update () {
		wave = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemy>().wave;

		GetComponent<TextMesh>().text = text + " " + wave; //displays wave number
	}
}
