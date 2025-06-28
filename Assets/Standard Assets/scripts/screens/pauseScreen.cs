using UnityEngine;
using System.Collections;

public class pauseScreen : MonoBehaviour {

	public GameObject options;

	public void ContinueClicked () { //take screen away pause screen if we press continue
		DataManager.PlayClick();

		Camera.main.GetComponent<stopGame>().PlayGame();
		Camera.main.GetComponent<GUI_Buttons>().paused = false;

		Destroy (this.gameObject);
	}

	public void Options () { //switch screen to options if we press opt
		DataManager.PlayClick();

		Instantiate (options, options.transform.position, options.transform.rotation);

		Destroy (this.gameObject);
	}

	public void ReturnToMenuClicked () { //return to menu if we press abort menu
		Time.timeScale = 1;

		DataManager.PlayClick();

		Application.LoadLevel (0);
	}
}
