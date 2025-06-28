using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

	public float Ysensitivity;
	public float Xsensitivity;

	public float minX;
	public float maxX;

	public float minY;
	public float maxY;

	float rotationX; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Ysensitivity;

		rotationX += Input.GetAxis("Mouse Y") * Xsensitivity;

		Mathf.Clamp(rotationX, minX, maxX);

		transform.localEulerAngles = new Vector3 (-rotationX, rotationY, 0);
	}
}
