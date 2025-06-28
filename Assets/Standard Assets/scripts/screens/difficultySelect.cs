using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class difficultySelect : MonoBehaviour {

	void Awake () {
		DataManager.PlayClick();
	}

	public void Begg () {
		DataManager.diff = "Beginner";

		loadlev();
	}

	public void Easy () {
		DataManager.diff = "Easy";

		loadlev();
	}

	public void Inter () {
		DataManager.diff = "Intermediate";

		loadlev();
	}

	public void Hard () {
		DataManager.diff = "Hard";

		loadlev ();
	}

	public void Impos () {
		DataManager.diff = "Imposable";

		loadlev ();
	}

	void loadlev () {
		SceneManager.LoadScene (DataManager.level);
	}
}
