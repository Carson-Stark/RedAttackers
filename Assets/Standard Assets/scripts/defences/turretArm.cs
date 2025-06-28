using UnityEngine;
using System.Collections;

public class turretArm : MonoBehaviour {
	
	void Update () {
		transform.rotation = new Quaternion (0, transform.parent.Find ("head").transform.rotation.y, 0, transform.parent.Find ("head").transform.rotation.w);
	}
}
