using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class failScreen : MonoBehaviour {

	string level;
	string difficulty;
	string wave;

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteKey("waveNumber");  //can't start where we left off if we press resume
		PlayerPrefs.DeleteKey("Money");
		PlayerPrefs.DeleteKey("Level");
		PlayerPrefs.DeleteKey("Diff");

		Camera.main.GetComponent<stats>().showStats = false;

		level = DataManager.level;
		difficulty = DataManager.diff;
		wave = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemy>().wave.ToString();

		try {
			transform.Find("Stats").GetComponent<Text>().text = "Level : " + level + "     Difficulty : " + difficulty;
			transform.Find("Stats 1").GetComponent<Text>().text = "Wave Reached : " + wave;
		}
		catch {
			Debug.LogError ("No 'stats' child was found : failScreen");
		}
	}

	public void OnTryAgainClicked () { //restart level when try again is clicked
		Camera.main.GetComponent<stats>().showStats = true;

		Time.timeScale = 1;
		DataManager.PlayClick();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnMenuCllicked () { //return to menu when mmenu is clicked
		Time.timeScale = 1;
 		DataManager.PlayClick();
		Application.LoadLevel (0);
	}
}
