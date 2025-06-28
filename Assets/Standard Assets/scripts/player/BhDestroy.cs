using UnityEngine;
using System.Collections;

public class BhDestroy : MonoBehaviour {

	public float BhLife;

	float count;
	
	void Awake () {
		count = BhLife;
	}
	
	// Update is called once per frame
	void Update () {
		count -= Time.deltaTime; //destroy timer

		if (count <= 0)
			Destroy(this.gameObject);
	}
}
