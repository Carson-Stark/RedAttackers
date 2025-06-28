using UnityEngine;
using System.Collections;

public class buttonRise : MonoBehaviour {

	public Transform risePoint;
	public Transform fallPoint;
	public float riseSpeed;

	bool pointerEnter;

	public void PEnter () {
		pointerEnter = true;
	}

	public void PLeave () {
		pointerEnter = false;
	}

	void Update () {
		if (pointerEnter)
			transform.position = Vector3.MoveTowards (transform.position, risePoint.position, riseSpeed);
		else
			transform.position = Vector3.MoveTowards (transform.position, fallPoint.position, riseSpeed);
	}
}
