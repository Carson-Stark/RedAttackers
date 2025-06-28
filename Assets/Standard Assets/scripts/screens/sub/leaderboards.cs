using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class leaderboards : MonoBehaviour {

	public Text[] difficulties;
	public string levelName;

	int[] HS;

	Text[] renderers;
	Text[] leadRenderers;

	bool leadSide;

	void Awake () {
		HS = PlayerPrefsX.GetIntArray(levelName + "HS", 0, difficulties.Length);

		int index = 0;
		foreach (Text diff in difficulties){
			diff.text = diff.text + HS[index];
			index++;
		}

		renderers = transform.parent.GetComponentsInChildren<Text>();
		leadRenderers = GetComponentsInChildren<Text>();

		leadSide = false;
	}

	public void tops () {
		foreach (Text rend in renderers){
			if (rend.gameObject.transform.tag != "top")
				rend.enabled = leadSide;
		}

		foreach (Text rend in leadRenderers){
			rend.enabled = !leadSide;
		}

		leadSide = !leadSide;
	}
}
