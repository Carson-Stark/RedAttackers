using UnityEngine;
using System.Collections;

public class transparency : MonoBehaviour {

	public float trans;

	Renderer rend;

	void Awake () {
		rend = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
		rend.material.color = new Color (rend.material.color.r, rend.material.color.g, rend.material.color.b, trans); //set gameobject to chosen trancperency
	}
}
