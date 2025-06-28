using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour {

	public float MoveSpeed;
	public float JumpPower;

	private Rigidbody rig;

	bool grounded;

	float xSpeed;
	float zSpeed;
	float ySpeed;

	void Awake(){
		rig = GetComponent<Rigidbody>(); //reference to rigidbody
		grounded = true; //player is on ground
	}

	// Update is called once per frame
	void Update () {
		if (grounded){
			xSpeed = Input.GetAxisRaw("Horizontal") * MoveSpeed; //check for movement input if on ground
			zSpeed = Input.GetAxisRaw("Vertical") * MoveSpeed;
		}

		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 1)) //if on ground
			grounded = true;
		else
			grounded = false;

		if (!grounded || hit.transform.tag == "Path")
			Camera.main.GetComponent<GUI_Buttons>().onPath = true;
		else
			Camera.main.GetComponent<GUI_Buttons>().onPath = false;

		if (Input.GetKeyDown (KeyCode.Space) && grounded) //if on ground and pressed space
			ySpeed = 1 * JumpPower;
		else
			ySpeed = 0 * JumpPower;
	}

	void FixedUpdate (){
		Debug.DrawRay(transform.position, transform.forward);

		if (grounded)
		rig.AddForce(transform.forward * zSpeed); //adds force to character
		rig.AddForce(transform.right * xSpeed);
		rig.AddForce(0, ySpeed, 0);
	}
}
