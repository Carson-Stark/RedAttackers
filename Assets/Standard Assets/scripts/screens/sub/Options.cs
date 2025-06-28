using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour {

	public GameObject pauseScreen;

	public GameObject multi_tx;
	public int[] multipliers;
	
	int count;

	void Awake () {
		count = PlayerPrefs.GetInt("lastFFSpeed");
		multi_tx.GetComponent<Text>().text = "X" + multipliers[count];
		Camera.main.GetComponent<GUI_Buttons>().ffSpeed = multipliers[count];
	}

	public void OnFF_Up () {
		ChangeSpeed(false);
	}

	public void OnFF_Down () {
		ChangeSpeed(true);
	}

	public void Cancel () {
		Instantiate(pauseScreen, pauseScreen.transform.position, pauseScreen.transform.rotation);
		Destroy (this.gameObject);
	}

	public void Save () {
		PlayerPrefs.SetInt("lastFFSpeed", count);
		Destroy (this.gameObject);
		Instantiate(pauseScreen, pauseScreen.transform.position, pauseScreen.transform.rotation);
	}

	void ChangeSpeed (bool down) {
		if (!down)
			count++;
		else
			count--;
		
		if (count > multipliers.Length - 1)
			count = 0;
		if (count <= -1)
			count = multipliers.Length - 1;

		multi_tx.GetComponent<Text>().text = "X" + multipliers[count];

		Camera.main.GetComponent<GUI_Buttons>().ffSpeed = multipliers[count];
	}
}
