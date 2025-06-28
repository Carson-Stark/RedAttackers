using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

	public AudioClip buttonClicked;

	public static int screenVisited = 0;
	public int playTime;

	void Awake () {
		screenVisited++;

		if (screenVisited >= playTime)
			AudioSource.PlayClipAtPoint(buttonClicked, transform.position);
	}
}
