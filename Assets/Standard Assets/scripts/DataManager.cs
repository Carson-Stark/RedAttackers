using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

	public static string level; //level name
	public static string diff;  //difficulty name
	public static bool resuming;

	public static AudioClip buttonClick; //public(static) click audio

	public bool canDeleteData;

	public AudioClip click; //click audio

	void Awake () {
		buttonClick = click; //set editor assinged audio public(static)
	}

	void Update () {
		if (canDeleteData && Input.GetKey (KeyCode.LeftControl) && Input.GetKey (KeyCode.LeftShift) && Input.GetKeyDown (KeyCode.Delete)){
			PlayerPrefs.DeleteAll();
			Debug.Log ("Data Deleted");
		}
	}

	public static void PlayClick () { //play audio
		AudioSource.PlayClipAtPoint (buttonClick, Camera.main.transform.position);
		Debug.Log ("audio");
	}
}
