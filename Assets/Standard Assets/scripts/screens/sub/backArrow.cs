using UnityEngine;
using System.Collections;

public class backArrow : MonoBehaviour {

	public int prevLeval;

	public void Clicked () {
		Application.LoadLevel (prevLeval);
	}
}
